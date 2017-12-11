


Imports System.Collections.Generic
Imports System.ComponentModel.Composition
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.InterceptionExtension


<Export(GetType(IIocManager))> _
Public Class IocManager
    Implements IIocManager


    Private m_IoCContainer As IUnityContainer
    Private m_Container As IUnityContainer
    Private discoverStrategy As DiscoveryStrategy
    Private deferredBuilds As List(Of Tuple(Of Type, Object))
    Private wildCharacters As String()
    Private assemblySearchWildCharacter As String = "WWTS"

    Public Property Container() As IUnityContainer
        Get
            Return m_Container
        End Get
        Private Set(value As IUnityContainer)
            m_Container = value
        End Set
    End Property


    Public Sub New()
        Me.New(DiscoveryStrategy.SearchBaseDirectory)
    End Sub


    Public Sub New(Optional defaultDiscoveryStrategy As DiscoveryStrategy = DiscoveryStrategy.SearchBaseDirectory, Optional namespaceWildCharacters As String = Nothing)
        If namespaceWildCharacters IsNot Nothing Then
            Me.assemblySearchWildCharacter = namespaceWildCharacters
        End If
        Me.discoverStrategy = defaultDiscoveryStrategy
        SetupContainer()
    End Sub

    Public Sub New(defaultDiscoveryStrategy As DiscoveryStrategy, assemblyWildCharacters As String())
        If assemblyWildCharacters Is Nothing OrElse assemblyWildCharacters.Length = 0 Then
            Throw New ArgumentException("Provide wild characters for assemblies loaded in application domain.")
        End If
        Me.wildCharacters = assemblyWildCharacters
        SetupContainer()
    End Sub

    Private Sub SetupContainer()
        Me.Container = New UnityContainer()
        Me.IoCContainer = Container
        Me.Container.RegisterInstance(Of IIocManager)(Me)
        DiscoverAssemblies()
    End Sub

    Private Sub DiscoverAssemblies()
        deferredBuilds = New List(Of Tuple(Of Type, Object))()
        Dim entryAssembly As Assembly = Assembly.GetEntryAssembly()

        If entryAssembly IsNot Nothing Then
            DiscoverExports(Assembly.GetEntryAssembly())
        End If

        Select Case discoverStrategy
            Case DiscoveryStrategy.SearchBaseDirectory
                ProcessBaseDirectory()
                Exit Select

            Case DiscoveryStrategy.Loaded
                ProcessAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(Function(assembly__1) assembly__1.FullName.Contains(assemblySearchWildCharacter) AndAlso Not assembly__1.FullName.Contains("DynamicProxies")))
                Exit Select
        End Select
        For Each build As Tuple(Of Type, Object) In deferredBuilds
            Me.Container.BuildUp(build.Item1, build.Item2)
        Next
    End Sub

    Private Sub ProcessBaseDirectory()
        Dim loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(Function(assemb) assemb.FullName.Contains(assemblySearchWildCharacter) AndAlso Not assemb.FullName.Contains("DynamicProxies")).ToDictionary((Function(assem) assem.Location))


        Dim assemblyFolder As String = AppDomain.CurrentDomain.BaseDirectory
        Dim assemblyFiles = Directory.EnumerateFiles(assemblyFolder, "*" + assemblySearchWildCharacter + "*.dll", SearchOption.AllDirectories)
        If wildCharacters IsNot Nothing AndAlso wildCharacters.Length > 0 Then
            For Each name As String In wildCharacters
                assemblyFiles = assemblyFiles.Union(Directory.EnumerateFiles(assemblyFolder, (Convert.ToString("*") & name) + "*.dll", SearchOption.AllDirectories))

            Next
        End If
        For Each fileName As String In assemblyFiles
            Dim assembly As Assembly
            If loadedAssemblies.ContainsKey(fileName) Then
                assembly = loadedAssemblies(fileName)
            Else
                Dim assemblyName__1 = AssemblyName.GetAssemblyName(fileName)
                assembly = AppDomain.CurrentDomain.Load(assemblyName__1)
            End If

            DiscoverExports(assembly)
        Next
    End Sub

    Private Sub ProcessAssemblies(assemblies As IEnumerable(Of Assembly))
        For Each assembly As Assembly In assemblies
            DiscoverExports(assembly)
        Next
    End Sub

    Private Sub DiscoverExports(assembly As Assembly)
        For Each type As Type In assembly.GetTypes()
            Dim exports = GetCustomAttribute(Of ExportAttribute)(type)
            If exports IsNot Nothing Then
                For Each export As ExportAttribute In exports
                    ProcessExport(type, export)
                    'Container.Configure<Interception>()
                    '                .AddPolicy("NamespaceMatchingPolicy")
                    '        .AddMatchingRule(new NamespaceMatchingRule("S10.Business.Impl"))
                    '                .AddCallHandler(new CustomPolicyBehavior())
                    '                ;


                Next
            End If
        Next
    End Sub

    Private Sub ProcessExport(type As Type, export As ExportAttribute)
        Dim contractType As Type = type
        If export IsNot Nothing Then
            Dim contractName As String = export.ContractName
            If export.ContractType IsNot Nothing Then
                contractType = export.ContractType
            End If

            Dim scopeAttributes = GetCustomAttribute(Of PartCreationPolicyAttribute)(type)
            If scopeAttributes IsNot Nothing Then
                Dim scope = scopeAttributes.FirstOrDefault()
                Dim manager As LifetimeManager = Nothing
                Container.AddNewExtension(Of Interception)()
                Select Case scope.CreationPolicy
                    Case CreationPolicy.[Shared]
                        manager = New ContainerControlledLifetimeManager()
                        Dim sharedObject = Activator.CreateInstance(type)
                        Container.RegisterInstance(contractType, contractName, sharedObject, manager)
                        Me.deferredBuilds.Add(New Tuple(Of Type, Object)(contractType, sharedObject))
                        Exit Select
                    Case Else

                        manager = New TransientLifetimeManager()

                        'Container.RegisterType(contractType, type, contractName, manager, new InjectionMember[]
                        '        {
                        '            new Interceptor<InterfaceInterceptor>(),
                        '            new InterceptionBehavior<PolicyInjectionBehavior>()
                        '        });
                        RegisterContract(type, contractType, contractName, manager)
                        Exit Select





                End Select
            Else
                Container.RegisterType(contractType, type, contractName)
            End If
        End If
    End Sub

    Protected Overridable Function RegisterContract(type As Type, contractType As Type, contractName As String, manager As LifetimeManager) As IUnityContainer
        'return Container.RegisterType(contractType, type, contractName, manager, new InjectionMember[]
        '                        {
        '                            new Interceptor<InterfaceInterceptor>(),
        '                            new InterceptionBehavior<ExceptionBehavior>()
        '                        });
        Return Container.RegisterType(contractType, type, contractName, manager)
    End Function

    ''' <summary>
    ''' This method explicitly only finds the first matching attribute.
    ''' We currently don't need to return multiple instances of the same attribute
    ''' </summary>
    ''' <typeparam name="TAttribute"></typeparam>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Shared Function GetCustomAttribute(Of TAttribute As Attribute)(type As Type) As IEnumerable(Of TAttribute)
        Dim results As IEnumerable(Of TAttribute) = Nothing
        Dim attributes = type.GetCustomAttributes(GetType(TAttribute), True)
        If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
            results = attributes.Cast(Of TAttribute)()
        End If
        Return results
    End Function

    Public Function Resolve(Of T)() As T Implements IIocManager.Resolve

        Return Me.Container.Resolve(Of T)()
    End Function

    Public Sub BuildUp(Of T)(existingObject As T) Implements IIocManager.BuildUp
        Me.Container.BuildUp(existingObject)
    End Sub

    Public Property IoCContainer() As IUnityContainer Implements IIocManager.IoCContainer

        Get
            Return m_IoCContainer
        End Get
        Set(value As IUnityContainer)
            m_IoCContainer = value
        End Set
    End Property
End Class
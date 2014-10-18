<System.ComponentModel.RunInstaller(True)> Partial Class FileArchiverProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.FileArchiverServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        Me.FileArchiverServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        '
        'FileArchiverServiceProcessInstaller
        '
        Me.FileArchiverServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.FileArchiverServiceProcessInstaller.Password = Nothing
        Me.FileArchiverServiceProcessInstaller.Username = Nothing
        '
        'FileArchiverServiceInstaller
        '
        Me.FileArchiverServiceInstaller.ServiceName = "FileArchive"
        '
        'FileArchiverProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.FileArchiverServiceProcessInstaller, Me.FileArchiverServiceInstaller})

    End Sub
    Friend WithEvents FileArchiverServiceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents FileArchiverServiceInstaller As System.ServiceProcess.ServiceInstaller

End Class

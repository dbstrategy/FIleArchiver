Imports System.Timers
Imports System.IO.Compression
Imports System.IO
Imports System.IO.Compression.ZipArchiveEntry


Public Class FileArchiver

    Private timer As Timer = Nothing

    Private Function Intit()

        Const strLOG_DIR As String = "\Logs"
        Const strSTART_MSG As String = "Application Started"
        Try
            With My.Application.Log.DefaultFileLogWriter
                .LogFileCreationSchedule = Logging.LogFileCreationScheduleOption.Daily
                .CustomLocation = Windows.Forms.Application.StartupPath & strLOG_DIR
                .AutoFlush = True
            End With

            Log(strSTART_MSG)

            Dim stratFolder As DirectoryInfo = Directory.CreateDirectory(My.Settings.stratPath)    'srategy folder path
            Dim perfFolder As DirectoryInfo = Directory.CreateDirectory(My.Settings.perfPath)      'performance folder path

            Log("Performance Matrix Archieving Started")
            CreateZipFolder(perfFolder, My.Settings.DaysToLookBack)
            Log("Performance Matrix Archieving Ends")

            Log("Strategy Matrix Archieving Started")
            CreateZipFolder(stratFolder, My.Settings.DaysToLookBack)
            Log("Strategy Matrix Archieving Ends")

            Log("Application Completed Successfully")
        Catch ex As Exception
            Log("There is an Error Form1_Load Function" & ex.Message)
        End Try

    End Function

    Public Sub CreateZipFolder(ByVal diRootFolder As DirectoryInfo, ByVal iDeleteOlderThenXDays As Integer)
        Try
            Dim strRootFolder1 As String = diRootFolder.FullName
            For Each diSubFolder As DirectoryInfo In diRootFolder.GetDirectories()

                Dim strFileName As String
                Dim strRootFolder As String = diSubFolder.FullName
                Dim strCurrentFolderDate As String = diSubFolder.Name.Trim
                Try
                    strCurrentFolderDate = Mid(strCurrentFolderDate, 1, 2) & "/" & Mid(strCurrentFolderDate, 3, 2) & "/" & Mid(strCurrentFolderDate, 5, 4)
                    Dim MyDateTime As DateTime = CDate(strCurrentFolderDate)
                    If MyDateTime < (DateAdd(DateInterval.Day, -iDeleteOlderThenXDays, Today())) Then
                        strFileName = diSubFolder.FullName & ".zip"
                        If File.Exists(strFileName) Then
                            Using zipToOpen As FileStream = New FileStream(strFileName, FileMode.Open)
                                Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Update)
                                    Dim readmeEntry As ZipArchiveEntry = archive.CreateEntry(strRootFolder, CompressionLevel.Optimal)
                                End Using
                            End Using
                        End If
                        ZipFile.CreateFromDirectory(strRootFolder, strFileName)
                        System.IO.Directory.Delete(strRootFolder, True) 'This function is to delete all files and its folder
                    End If
                Catch ex As Exception
                    Log("Folder is Not Valid " & ex.Message)
                End Try
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Log(ByVal message As String)
        'logs passed in message
        My.Application.Log.WriteEntry(String.Format("{0}    {1}", Now.ToString, message))
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        Log("FileArchiver Started")
        Intit()
        timer = New Timer()
        Me.timer.Interval = 1000  ' * 60 * 60 * 6
        Me.timer.Enabled = True
        AddHandler Me.timer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.timer_Tick)
    End Sub

    Protected Overrides Sub OnStop()
        Log("FileArchiver Stop")
        timer.Enabled = False
    End Sub

    Private Sub timer_Tick(ByVal sender As Object, ByVal e As ElapsedEventArgs)

        'Set the enabled property to false.
        timer.Enabled = False
        Intit()
        'Enable the timer.        
        timer.Enabled = True
    End Sub
    'Public Function CreateEntry(entryName As String) As ZipArchiveEntry

    'End Function
End Class


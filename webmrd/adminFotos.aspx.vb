Imports WebMRD.Utilities
Imports System.IO
'Imports ImageResizer
Imports System.Web.UI.WebControls


Partial Public Class adminFotos
    Inherits System.Web.UI.Page


    Public Function detectBrowserVersion(ByVal browser As String) As String

        Dim version As String = ""
        version = Request.UserAgent.Substring(Request.UserAgent.IndexOf(browser))
        Try
            version = version.Substring(0, version.IndexOf(" "))
        Catch ex As Exception

        End Try
        version = version.Replace(browser, "")
        version = version.Replace("/", "").Trim
        Return version

    End Function


    Public Function detectComplaintBrowserCapabilities() As String

        If Request.Browser.EcmaScriptVersion.ToString() < 1 Then
            'lnkLogin.Enabled = False
            Return getMessage(Me, "Javascripts")
        End If

        If Request.Browser.Cookies = False Then
            'lnkLogin.Enabled = False
            Return getMessage(Me, "Cookies")
        End If

        Return ""

    End Function


    Public Sub detectBrowserUsed()

        If Request.UserAgent.Contains("MSIE 9.0") Then

            imgBrowser.ImageUrl = "images/ie913x13.png"
            imgBrowser.ToolTip = getMessage(Me, "IE9")

        ElseIf Request.UserAgent.Contains("MSIE 8.0") Then

            imgBrowser.ImageUrl = "images/ie813x13.png"
            imgBrowser.ToolTip = getMessage(Me, "IE8")

        ElseIf Request.UserAgent.Contains("MSIE 7.0") Then

            imgBrowser.ImageUrl = "images/ie613x13.png"
            imgBrowser.ToolTip = getMessage(Me, "IE7")

        ElseIf Request.UserAgent.Contains("MSIE 6.0") Then

            imgBrowser.ImageUrl = "images/ie613x13.png"
            imgBrowser.ToolTip = getMessage(Me, "IE6")

        ElseIf Request.UserAgent.Contains("Firefox") Then

            imgBrowser.ImageUrl = "images/firefox13x13.png"
            imgBrowser.ToolTip = getMessage(Me, "Firefox") & detectBrowserVersion("Firefox")

        ElseIf Request.UserAgent.Contains("Chrome") Then

            imgBrowser.ImageUrl = "images/chrome13x13.png"
            imgBrowser.ToolTip = getMessage(Me, "Chrome") & detectBrowserVersion("Chrome")

        ElseIf Request.UserAgent.Contains("Safari") Then

            imgBrowser.ImageUrl = "images/safari13x13.png"
            imgBrowser.ToolTip = getMessage(Me, "Safari") & detectBrowserVersion("Safari")

        End If

        lblRestrictions.Text = detectComplaintBrowserCapabilities()

        If lblRestrictions.Text = "" Then

            lblRestrictions.Text = getMessage(Me, "AlternateSlideshowView")

        End If

        pnlRestrictions.Visible = True

    End Sub


    Public Function detectBrowserLanguage() As String

        Return Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").Substring(0, 5).Replace("-", "").ToLower

    End Function


    Public Function findRemoteIP() As String

        Dim ip As String = "0.0.0.0"

        ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR")

        If Not String.IsNullOrEmpty(ip) Then

            Dim ipRange() As String = ip.Split(",")
            Dim le As Integer = ipRange.Length - 1
            Dim TrueIP As String = ipRange(le)
            'Dim TrueIP As String =  ipRange(0) 

        Else

            ip = Request.ServerVariables("REMOTE_ADDR")

        End If

        Return ip

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim systemStatus As String = ""
            systemStatus = getSystemStatus()

            If Session("username") = "" Or Session("username") = "Anonymous" Then
                cleanSession(Me)
                Response.Redirect("login.aspx")
            End If

            If systemStatus.Contains("Offline") Then

                ddLanguage.Items.Add("Español")
                ddLanguage.SelectedIndex = 1
                ddLanguage.Enabled = False

                ddLanguage.ToolTip = systemStatus

            Else

                fillDropDownList(ddLanguage, "SELECT slangid, slangname FROM languages ORDER BY 2")
                If Session("preferredLanguage") = "" And (Session("username") = "" Or Session("username") = "Anonymous") Then
                    Session("preferredLanguage") = detectBrowserLanguage()
                End If
                ddLanguage.SelectedValue = Session("preferredLanguage")
                detectBrowserUsed()
                setLabels(Me)

                If Session("preferredLanguage") = "esmx" Then
                    pnlSlogan.BackImageUrl = "images/web_08_complete.jpg"
                ElseIf Session("preferredLanguage") = "enus" Then
                    pnlSlogan.BackImageUrl = "images/web_08_complete_en.jpg"
                End If

                If verifyIfUserIsReallyLogged(Me) = False Then
                    Response.Redirect("login.aspx")
                End If

                Dim uploadingUser As String = Session("username")

                Dim dsDeleteTempUploadedImages As DataSet = getSQLQueryAsDataset("SELECT iimageid, simageurl FROM tempuploadedimages WHERE suploadinguser = '" & uploadingUser & "' and breadytodelete = 1")

                For i = 0 To dsDeleteTempUploadedImages.Tables(0).Rows.Count - 1

                    Dim fileToDelete As String = Server.MapPath("~/uploadtemp") & "/" & dsDeleteTempUploadedImages.Tables(0).Rows(i).Item(1).ToString
                    Dim result As Boolean = True

                    Try
                        File.Delete(fileToDelete)
                    Catch ex As Exception
                        result = False
                    End Try

                    If result = True Then
                        executeSQLCommand("DELETE FROM tempuploadedimages WHERE iimageid = " & dsDeleteTempUploadedImages.Tables(0).Rows(i).Item(0).ToString)
                    End If
                    
                Next i


                fillDropDownList(ddGalleries, "SELECT igalleryid, galleryname_" & Session("preferredLanguage") & " FROM photogalleries")

                If Session("SelectedGallery") Is Nothing Then
                    Session("SelectedGallery") = "1" 'housesextimages
                End If

                ddGalleries.SelectedIndex = Session("SelectedGallery") - 1

                gvImages.DataSource = getSQLQueryAsDataset("SELECT iimageid, REPLACE(simageurl, '/', '/smaller/') as simageurl FROM " & getSQLQueryAsString("SELECT stablename FROM photogalleries WHERE igalleryid = " & Session("SelectedGallery")))
                gvImages.DataBind()

                If Session("UploadStatus") IsNot Nothing Then
                    lblUploadStatus.Text = getMessage(Me, "lblSuccess")
                End If


            End If

        End If

    End Sub


    Protected Sub ddGalleries_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddGalleries.SelectedIndexChanged

        Session("UploadStatus") = ""
        lblUploadStatus.Text = ""

        Session("SelectedGallery") = ddGalleries.SelectedIndex + 1

        gvImages.DataSource = getSQLQueryAsDataset("SELECT iimageid, REPLACE(simageurl, '/', '/smaller/') as simageurl FROM " & getSQLQueryAsString("SELECT stablename FROM photogalleries WHERE igalleryid = " & Session("SelectedGallery")))
        gvImages.DataBind()

    End Sub


    Protected Sub ddLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddLanguage.SelectedIndexChanged

        Session("preferredLanguage") = ddLanguage.SelectedValue

        setLabels(Me)
        detectBrowserUsed()

        If Session("preferredLanguage") = "esmx" Then

            pnlSlogan.BackImageUrl = "images/web_08_complete.jpg"

        ElseIf Session("preferredLanguage") = "enus" Then

            pnlSlogan.BackImageUrl = "images/web_08_complete_en.jpg"

        End If

    End Sub


    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs) Handles lnkLogout.Click

        logLogout(Me, Session("username"), findRemoteIP(), Session("userMachineName"))
        Response.Redirect("index.aspx")

    End Sub


    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

        Dim filename As String = ""
        Dim pedazos() As String = Me.Request.FilePath.Split("/")
        Dim page As String = pedazos(pedazos.Length - 1)

        Dim fecha As String = ""
        Dim hora As String = ""

        Dim ilogid As Integer = 1

        Session("UploadStatus") = ""

        If fUpload.HasFile Or fUpload2.HasFile Or fUpload3.HasFile Or fUpload4.HasFile Then

            Try

                fecha = getMySQLFullDate()

                ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

                executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Starting upload batch...', '0')")

                Dim fileExtension As String
                Dim fileExtension2 As String
                Dim fileExtension3 As String
                Dim fileExtension4 As String

                fileExtension = Path.GetExtension(fUpload.FileName).ToLower
                fileExtension2 = Path.GetExtension(fUpload2.FileName).ToLower
                fileExtension3 = Path.GetExtension(fUpload3.FileName).ToLower
                fileExtension4 = Path.GetExtension(fUpload4.FileName).ToLower

                Dim tableToUpdate As String = getSQLQueryAsString("SELECT stablename FROM photogalleries WHERE igalleryid = " & Session("SelectedGallery"))

                If (fileExtension <> ".jpg" Or fileExtension <> ".png" Or fileExtension <> ".jpeg" Or fileExtension2 <> ".jpg" Or fileExtension2 <> ".png" Or fileExtension2 <> ".jpeg" Or fileExtension3 <> ".jpg" Or fileExtension3 <> ".png" Or fileExtension3 <> ".jpeg" Or fileExtension4 <> ".jpg" Or fileExtension4 <> ".png" Or fileExtension4 <> ".jpeg") = True Then

                    Dim photosToUpload As HttpFileCollection = Request.Files

                    For p As Integer = 0 To photosToUpload.Count - 1

                        Dim photoJustUploaded As HttpPostedFile = photosToUpload(p)

                        If photoJustUploaded.ContentLength <= 0 Then
                            Continue For
                        End If

                        If photoJustUploaded.ContentLength < 20971520 Then

                            Dim dsGalleries As DataSet = getSQLQueryAsDataset("SELECT stablename FROM photogalleries")

                            Dim dsPossibleNames As DataSet = New DataSet
                            Dim dTable As New DataTable("Ids")
                            dTable.Columns.Add("PossibleIds", System.Type.GetType("System.Int32"))
                            dsPossibleNames.Tables.Add(dTable)

                            For i = 0 To dsGalleries.Tables(0).Rows.Count - 1

                                Dim dRow As DataRow = dTable.NewRow()
                                dRow(0) = getSQLQueryAsInteger("SELECT MAX(iimageid) + 1 AS inextimageid FROM " & dsGalleries.Tables(0).Rows(i).Item(0).ToString)
                                dTable.Rows.Add(dRow)

                            Next i

                            Dim higherAvailableNumber As Integer

                            For j = 0 To dsPossibleNames.Tables(0).Rows.Count - 1

                                If j = 0 Then
                                    higherAvailableNumber = CInt(dsPossibleNames.Tables(0).Rows(j).Item(0).ToString)
                                End If

                                If CInt(dsPossibleNames.Tables(0).Rows(j).Item(0).ToString) > higherAvailableNumber Then
                                    higherAvailableNumber = CInt(dsPossibleNames.Tables(0).Rows(j).Item(0).ToString)
                                End If

                            Next j

                            If dsPossibleNames.Tables(0).Rows.Count <= 0 Then

                                lblUploadStatus.Text = getMessage(Me, "lblWarningErrorCountingGalleries")

                                fecha = getMySQLFullDate()

                                ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

                                executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Error while counting galleries : " & tableToUpdate & "/" & filename & "', '0')")

                                Exit Sub

                            End If


                            filename = higherAvailableNumber + p & ".jpg"

                            photoJustUploaded.SaveAs(Server.MapPath("~/uploadtemp") & "/" & filename)

                            SaveResizedImages(399, 299, Server.MapPath("~/uploadtemp") & "/" & filename, Server.MapPath("~/") & tableToUpdate & "/" & filename)
                            SaveResizedImages(1024, 768, Server.MapPath("~/uploadtemp") & "/" & filename, Server.MapPath("~/") & tableToUpdate & "/larger/" & filename)
                            SaveResizedImages(67, 50, Server.MapPath("~/uploadtemp") & "/" & filename, Server.MapPath("~/") & tableToUpdate & "/smaller/" & filename)

                            Session("UploadStatus") = getMessage(Me, "lblSuccess")

                            photoJustUploaded = Nothing

                            executeSQLCommand("INSERT INTO tempuploadedimages VALUES (0, " & getSQLQueryAsInteger("SELECT MAX(iimageid) + 1 AS inextimageid FROM tempuploadedimages") & ", '" & Session("username") & "', '" & getMySQLFullDate() & "', '" & filename & "', '', '', 0)")

                            fecha = getMySQLFullDate()

                            ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

                            executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Image uploaded : " & tableToUpdate & "/" & filename & "', '0')")

                        Else

                            lblUploadStatus.Text = getMessage(Me, "lblWarningFileSize")

                            fecha = getMySQLFullDate()

                            ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

                            executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Image size too big : " & tableToUpdate & "/" & filename & "', '0')")

                        End If

                    Next p

                Else 'Trying to upload files that aren't image files...

                    lblUploadStatus.Text = getMessage(Me, "lblWarningOnlyImages")

                    fecha = getMySQLFullDate()

                    ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

                    executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'User tried to upload non-image file : " & tableToUpdate & "/" & filename & "', '0')")

                End If

            Catch ex1 As System.Threading.ThreadAbortException

                'Nothing, its just the page reload when it finishes the upload that kills the thread (its necessary for the Success! message

            Catch ex As Exception

                'lblUploadStatus.Text = "Error"
                executeSQLCommand("INSERT INTO errorlogs VALUES ('" & getMySQLFullDate() & "', '" & filename & " upload by " & Session("username") & " caused the following exception : " & ex.ToString.Replace("--", "").Replace("'", "") & "'")
                executeSQLCommand("INSERT INTO tempuploadedimages VALUES (0, " & getSQLQueryAsInteger("SELECT MAX(iimageid) + 1 AS inextimageid FROM tempuploadedimages") & ", '" & Session("username") & "', '" & getMySQLFullDate() & "', '" & filename & "', '', '', 1)")

            End Try

            fecha = getMySQLFullDate()

            ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

            executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Finished upload batch...', '0')")


            detectNewUploadedImages()
            Response.Redirect("adminFotos.aspx")


        End If

    End Sub


    Private Function detectNewUploadedImages()

        Dim result As Boolean = False

        Dim pedazos() As String = Me.Request.FilePath.Split("/")
        Dim page As String = pedazos(pedazos.Length - 1)

        Dim fecha As String = ""
        Dim hora As String = ""

        Dim ilogid As Integer = 1

        Dim dsGalleries As DataSet
        dsGalleries = getSQLQueryAsDataset("SELECT stablename FROM photogalleries")

        Dim dsPhotosInGallery As DataSet
        Dim foundPhotoInDB As Boolean = False

        For i = 0 To dsGalleries.Tables(0).Rows.Count - 1

            Dim files() As String = Directory.GetFiles(Server.MapPath("") & "/" & dsGalleries.Tables(0).Rows(i).Item(0).ToString())
            Dim filesLarger() As String = Directory.GetFiles(Server.MapPath("") & "/" & dsGalleries.Tables(0).Rows(i).Item(0).ToString() & "/larger")
            Dim filesSmaller() As String = Directory.GetFiles(Server.MapPath("") & "/" & dsGalleries.Tables(0).Rows(i).Item(0).ToString() & "/smaller")

            dsPhotosInGallery = getSQLQueryAsDataset("SELECT simageurl FROM " & dsGalleries.Tables(0).Rows(i).Item(0).ToString())

            For j = 0 To files.Length - 1

                For k = 0 To dsPhotosInGallery.Tables(0).Rows().Count - 1

                    foundPhotoInDB = False

                    If files(j).Contains(dsPhotosInGallery.Tables(0).Rows(k).Item(0).ToString.Replace(dsGalleries.Tables(0).Rows(i).Item(0).ToString() & "/", "")) = True Then

                        foundPhotoInDB = True
                        Exit For

                    End If

                Next k

                If foundPhotoInDB = False And (files(j).EndsWith(".jpg") Or files(j).EndsWith(".png") Or files(j).EndsWith(".gif") Or files(j).EndsWith(".bmp") Or files(j).EndsWith(".tif")) Then


                    Try

                        If (filesSmaller(j).EndsWith(".jpg") Or filesSmaller(j).EndsWith(".png") Or filesSmaller(j).EndsWith(".gif") Or filesSmaller(j).EndsWith(".bmp") Or filesSmaller(j).EndsWith(".tif")) = True Then

                            If (filesLarger(j).EndsWith(".jpg") Or filesLarger(j).EndsWith(".png") Or filesLarger(j).EndsWith(".gif") Or filesLarger(j).EndsWith(".bmp") Or filesLarger(j).EndsWith(".tif")) = True Then

                                Dim folderAndFileNameOnly As String = files(j).Substring(files(j).IndexOf(dsGalleries.Tables(0).Rows(i).Item(0).ToString())).Replace("\", "/")

                                fecha = getMySQLFullDate()

                                executeSQLCommand("INSERT INTO " & dsGalleries.Tables(0).Rows(i).Item(0).ToString() & " VALUES (0, " & getSQLQueryAsInteger("SELECT MAX(iimageid) + 1 AS inextimageid FROM " & dsGalleries.Tables(0).Rows(i).Item(0).ToString()) & ", '" & Session("username") & "', '" & fecha & "', '" & folderAndFileNameOnly & "', '', '')")

                                ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

                                executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Image found in filesystem and added to db : " & folderAndFileNameOnly & "', '0')")

                                result = True

                            End If

                        End If

                    Catch ex As Exception

                        'There are more files in one subfolder than in the root one. User Mistake? Deleting temp files...
                        executeSQLCommand("INSERT INTO errorlogs VALUES ('" & getMySQLFullDate() & "', 'The following image is repeated more than necessary in the gallery folders: " & files(j) & "'")

                    End Try

                End If

            Next j

        Next i

        executeSQLCommand("UPDATE tempuploadedimages SET breadytodelete = 1 WHERE suploadinguser = '" & Session("username") & "'")

        Return result

    End Function


    Public Sub SaveResizedImages(Width As Integer, Height As Integer, imageUrl As String, destPath As String)

        Dim resizedPhoto As System.Drawing.Image = Nothing
        Dim originalImage As System.Drawing.Image = System.Drawing.Image.FromFile(imageUrl)

        resizedPhoto = ResizeImages(originalImage, Width, Height)
        resizedPhoto.Save(destPath, Drawing.Imaging.ImageFormat.Jpeg)

        originalImage.Dispose()
        resizedPhoto.Dispose()

        originalImage = Nothing
        resizedPhoto = Nothing

    End Sub


    Public Function ResizeImages(imgPhoto As System.Drawing.Image, Width As Integer, Height As Integer) As System.Drawing.Image

        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height
        Dim sourceX As Integer = 0
        Dim sourceY As Integer = 0
        Dim destX As Integer = 0
        Dim destY As Integer = 0

        Dim nPercent As Single = 0
        Dim nPercentW As Single = 0
        Dim nPercentH As Single = 0

        nPercentW = (CSng(Width) / CSng(sourceWidth))
        nPercentH = (CSng(Height) / CSng(sourceHeight))
        If nPercentH < nPercentW Then
            nPercent = nPercentH
            destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2)
        Else
            nPercent = nPercentW
            destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2)
        End If

        Dim destWidth As Integer = CInt(Math.Truncate(sourceWidth * nPercent))
        Dim destHeight As Integer = CInt(Math.Truncate(sourceHeight * nPercent))

        Dim bmPhoto As New Drawing.Bitmap(Width, Height, Drawing.Imaging.PixelFormat.Format24bppRgb)
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

        Dim grPhoto As Drawing.Graphics = Drawing.Graphics.FromImage(bmPhoto)
        grPhoto.Clear(Drawing.Color.Transparent)
        grPhoto.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic

        grPhoto.DrawImage(imgPhoto, New Drawing.Rectangle(destX, destY, destWidth, destHeight), New Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), Drawing.GraphicsUnit.Pixel)

        grPhoto.Dispose()
        Return bmPhoto

    End Function

   

    Private Sub gvImages_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvImages.RowCommand

        Session("UploadStatus") = ""

        Dim pedazos() As String = Me.Request.FilePath.Split("/")
        Dim page As String = pedazos(pedazos.Length - 1)

        Dim fecha As String = ""
        Dim hora As String = ""

        Dim ilogid As Integer = 1
        fecha = getMySQLFullDate()

        lblUploadStatus.Text = ""

        Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, System.Web.UI.WebControls.Button).NamingContainer, GridViewRow)

        Dim queries(3) As String
        Dim imageBeingMoved As Integer = CInt(gvImages.Rows(row.RowIndex).Cells(1).Text)
        Dim imageCount As Integer = gvImages.Rows.Count
        Dim tableToUpdate As String = getSQLQueryAsString("SELECT stablename FROM photogalleries WHERE igalleryid = " & Session("SelectedGallery"))

        If e.CommandName = "Up" Then

            If imageBeingMoved = 1 Then
                Exit Sub 'Can't move up the first line, its already at the top
            End If

            queries(0) = "UPDATE " & tableToUpdate & " SET iimageid = " & (imageBeingMoved - 1) * 1000 & " WHERE iimageid = " & imageBeingMoved - 1
            queries(1) = "UPDATE " & tableToUpdate & " SET iimageid = " & imageBeingMoved - 1 & " WHERE iimageid = " & imageBeingMoved
            queries(2) = "UPDATE " & tableToUpdate & " SET iimageid = " & imageBeingMoved & " WHERE iimageid = " & (imageBeingMoved - 1) * 1000

            executeTransactedSQLCommand(queries)

            ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

            executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Image moved up : " & imageBeingMoved & "', '0')")

            'gvImages.DataSource = getSQLQueryAsDataset("SELECT iimageid, REPLACE(simageurl, '/', '/smaller/') as simageurl FROM " & tableToUpdate)
            'gvImages.DataBind()
            Response.Redirect("adminFotos.aspx")

        ElseIf e.CommandName = "Down" Then

            If imageBeingMoved = imageCount Then
                Exit Sub 'Can't move down the last line, its already at the bottom
            End If

            queries(0) = "UPDATE " & tableToUpdate & " SET iimageid = " & (imageBeingMoved + 1) * 1000 & " WHERE iimageid = " & imageBeingMoved + 1
            queries(1) = "UPDATE " & tableToUpdate & " SET iimageid = " & imageBeingMoved + 1 & " WHERE iimageid = " & imageBeingMoved
            queries(2) = "UPDATE " & tableToUpdate & " SET iimageid = " & imageBeingMoved & " WHERE iimageid = " & (imageBeingMoved + 1) * 1000

            executeTransactedSQLCommand(queries)

            ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

            executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Image moved down : " & imageBeingMoved & "', '0')")

            'gvImages.DataSource = getSQLQueryAsDataset("SELECT iimageid, REPLACE(simageurl, '/', '/smaller/') as simageurl FROM " & tableToUpdate)
            'gvImages.DataBind()
            Response.Redirect("adminFotos.aspx")

        ElseIf e.CommandName = "Delete" Then

            Dim imagesrc As String = getSQLQueryAsString("SELECT simageurl FROM " & tableToUpdate & " WHERE iimageid = " & imageBeingMoved)

            Try

                File.Delete(Server.MapPath("~/" & imagesrc))
                File.Delete(Server.MapPath("~/" & imagesrc.Replace("/", "/smaller/")))
                File.Delete(Server.MapPath("~/" & imagesrc.Replace("/", "/larger/")))

            Catch ex As Exception

            End Try

            executeSQLCommand("DELETE FROM " & tableToUpdate & " WHERE iimageid = " & imageBeingMoved)

            ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

            executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Image deleted : " & imagesrc & "', '0')")

            Response.Redirect("adminFotos.aspx")


        ElseIf e.CommandName = "Move" Then

            Dim imagesrc As String = getSQLQueryAsString("SELECT simageurl FROM " & tableToUpdate & " WHERE iimageid = " & imageBeingMoved)

            Try

                Dim ddl As DropDownList = DirectCast(row.Cells(3).FindControl("ddGalleriesToMove"), DropDownList)

                If ddl.SelectedIndex + 1 = Session("SelectedGallery") Then
                    'Do Nothing, you are already at that gallery
                Else

                    Dim strNewLocation As String = getSQLQueryAsString("SELECT simageurl FROM " & getSQLQueryAsString("SELECT stablename FROM photogalleries WHERE igalleryid = " & ddl.SelectedIndex + 1 & " LIMIT 1"))
                    Dim strFileName As String = ""

                    strNewLocation = strNewLocation.Substring(0, strNewLocation.IndexOf("/"))
                    strFileName = imagesrc.Substring(imagesrc.IndexOf("/") + 1)

                    File.Move(Server.MapPath("~/" & imagesrc), Server.MapPath("~/" & strNewLocation & "/" & strFileName))
                    File.Move(Server.MapPath("~/" & imagesrc.Replace("/", "/smaller/")), Server.MapPath("~/" & strNewLocation & "/smaller/" & strFileName))
                    File.Move(Server.MapPath("~/" & imagesrc.Replace("/", "/larger/")), Server.MapPath("~/" & strNewLocation & "/larger/" & strFileName))

                    executeSQLCommand("DELETE FROM " & tableToUpdate & " WHERE iimageid = " & imageBeingMoved)

                    ilogid = getSQLQueryAsInteger("SELECT IF(MAX(ilogid) + 1 IS NULL, 1, MAX(ilogid) + 1) AS ilogid FROM sqllogs ORDER BY querydate DESC LIMIT 1")

                    executeSQLCommand("INSERT INTO sqllogs VALUES(" & ilogid & ", '" & fecha & "', '" & Session("username") & "', '" & Session.SessionID & "', '" & Session("userIP") & "', '" & Session("userMachineName") & "', 'Image moved to new gallery : " & imagesrc & " to the folder " & strNewLocation & ". Deleted it from db so scan can readd it', '0')")

                    detectNewUploadedImages()

                End If

                

            Catch ex As Exception

            End Try

            Response.Redirect("adminFotos.aspx")

        End If

    End Sub


    Private Sub gvImages_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvImages.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btn As Button = DirectCast(e.Row.Cells(1).FindControl("btnDelete"), Button)
            btn.OnClientClick = "javascript:return confirm('" & getMessage(Me, "lblWarningDelete") & "');"

            Dim btn2 As Button = DirectCast(e.Row.Cells(3).FindControl("btnUp"), Button)
            btn2.Text = getMessage(Me, "btnUp")

            Dim btn3 As Button = DirectCast(e.Row.Cells(3).FindControl("btnDown"), Button)
            btn3.Text = getMessage(Me, "btnDown")

            Dim lbl As Label = DirectCast(e.Row.Cells(3).FindControl("lblMoveTo"), Label)
            lbl.Text = getMessage(Me, "lblMoveTo")

            Dim btn4 As Button = DirectCast(e.Row.Cells(3).FindControl("btnMove"), Button)
            btn4.Text = getMessage(Me, "btnMove")
            btn4.OnClientClick = "javascript:return confirm('" & getMessage(Me, "lblWarningMove") & "');"


            Dim ddl As DropDownList = DirectCast(e.Row.Cells(3).FindControl("ddGalleriesToMove"), DropDownList)

            For i = 0 To ddGalleries.Items.Count - 1

                ddl.Items.Add(New ListItem(ddGalleries.Items.Item(i).Text, ddGalleries.Items.Item(i).Value))

            Next

            ddl.DataBind()

        ElseIf e.Row.RowType = DataControlRowType.Header Then

            For i As Integer = 0 To e.Row.Cells.Count - 1

                e.Row.Cells(i).Text = getMessage(Me, "columnHeader" & i)

            Next

        End If

    End Sub


End Class
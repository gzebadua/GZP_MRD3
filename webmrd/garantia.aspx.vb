Partial Public Class garantia
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


    'Public Sub detectBrowserUsed()

    '    If Request.UserAgent.Contains("MSIE 9.0") Then

    '        imgBrowser.ImageUrl = "images/ie913x13.png"
    '        imgBrowser.ToolTip = getMessage(Me, "IE9")

    '    ElseIf Request.UserAgent.Contains("MSIE 8.0") Then

    '        imgBrowser.ImageUrl = "images/ie813x13.png"
    '        imgBrowser.ToolTip = getMessage(Me, "IE8")

    '    ElseIf Request.UserAgent.Contains("MSIE 7.0") Then

    '        imgBrowser.ImageUrl = "images/ie613x13.png"
    '        imgBrowser.ToolTip = getMessage(Me, "IE7")

    '    ElseIf Request.UserAgent.Contains("MSIE 6.0") Then

    '        imgBrowser.ImageUrl = "images/ie613x13.png"
    '        imgBrowser.ToolTip = getMessage(Me, "IE6")

    '    ElseIf Request.UserAgent.Contains("Firefox") Then

    '        imgBrowser.ImageUrl = "images/firefox13x13.png"
    '        imgBrowser.ToolTip = getMessage(Me, "Firefox") & detectBrowserVersion("Firefox")

    '    ElseIf Request.UserAgent.Contains("Chrome") Then

    '        imgBrowser.ImageUrl = "images/chrome13x13.png"
    '        imgBrowser.ToolTip = getMessage(Me, "Chrome") & detectBrowserVersion("Chrome")

    '    ElseIf Request.UserAgent.Contains("Safari") Then

    '        imgBrowser.ImageUrl = "images/safari13x13.png"
    '        imgBrowser.ToolTip = getMessage(Me, "Safari") & detectBrowserVersion("Safari")

    '    End If

    '    lblRestrictions.Text = detectComplaintBrowserCapabilities()

    '    If lblRestrictions.Text = "" Then

    '        lblRestrictions.Text = getMessage(Me, "AlternateSlideshowView")

    '    End If

    '    pnlRestrictions.Visible = True

    'End Sub


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

        '<iframe name="nabaztagcall" frameborder="0" scrolling="auto" width="0px" height="0px" style="position: absolute; top: 0px; left: 0px;" src="http://api.nabaztag.com/vl/FR/api.jsp?sn=0013d38621bd&token=1217250961&chortitle=WebpageVisitor&ttlive=0&chor=10,0,motor,1,180,0,0,0,motor,0,180,0,0,20,motor,1,0,0,0,20,motor,0,0,0,0,40,motor,1,180,0,0,40,motor,0,180,0,0,60,motor,1,0,0,0,60,motor,0,0,0,0,0,led,4,255,255,0,2,led,3,255,128,0,4,led,2,255,0,0,6,led,1,0,0,255,8,led,0,128,0,128,10,led,4,34,139,34,12,led,3,255,255,0,14,led,2,255,128,0,16,led,1,255,0,0,18,led,0,0,0,255,20,led,4,128,0,128,22,led,3,34,139,34,24,led,2,255,255,0,26,led,1,255,128,0,28,led,0,255,0,0,30,led,4,0,0,255,32,led,3,128,0,128,34,led,2,34,139,34,36,led,1,255,255,0,38,led,0,255,128,0,40,led,4,255,0,0,42,led,3,0,0,255,44,led,2,128,0,128,46,led,1,34,139,34,48,led,0,255,255,0,50,led,4,255,128,0,52,led,3,255,0,0,54,led,2,0,0,255,56,led,1,128,0,128,58,led,0,34,139,34,60,led,4,255,255,0,62,led,3,255,128,0,64,led,2,255,0,0,66,led,1,0,0,255,68,led,0,128,0,128,70,led,4,34,139,34,72,led,3,255,255,0,74,led,2,255,128,0,76,led,1,255,0,0,78,led,0,0,0,255,80,led,4,128,0,128,82,led,3,34,139,34,84,led,2,255,255,0"></iframe> 

        If Not Page.IsPostBack Then

            Dim systemStatus As String = ""
            systemStatus = getSystemStatus()

            If Session("username") = "" Or Session("username") = "Anonymous" Then
                Session("username") = "Anonymous"
            End If

            If systemStatus.Contains("Offline") Then

                'pnlOutOfService.Visible = True
                'If systemStatus.Contains("System") Then
                'lblSystemOut.Text = getMessage(Me, "lblSystemOut")
                'End If
                'pnlLogin.Visible = False
                'pnlWelcome.Visible = False
                'pnlOfflineMenu.Visible = True
                ddLanguage.Items.Add("Español")
                ddLanguage.SelectedIndex = 1
                ddLanguage.Enabled = False

            Else

                'detectBrowserUsed()
                fillDropDownList(ddLanguage, "SELECT slangid, slangname FROM languages ORDER BY 2")
                If Session("preferredLanguage") = "" And (Session("username") = "" Or Session("username") = "Anonymous") Then
                    Session("preferredLanguage") = detectBrowserLanguage()
                End If
                ddLanguage.SelectedValue = Session("preferredLanguage")
                setLabels(Me)

                If Session("preferredLanguage") = "esmx" Then
                    pnlSlogan.BackImageUrl = "images/web_08_complete.jpg"
                    pnlGarantia.BackImageUrl = "images/web_15.png"
                ElseIf Session("preferredLanguage") = "enus" Then
                    pnlSlogan.BackImageUrl = "images/web_08_complete_en.jpg"
                    pnlGarantia.BackImageUrl = "images/web_15_en.png"
                End If

                'Session("menuCurrentPageIndex") = 0
                'Session("menuPageSize") = 6
                'fillDataListNotPaged(0, dlMenu, "SELECT ilinkid, slinkimage, slinkurl, slinkdescription_" & Session("preferredLanguage") & ", slinktooltip_" & Session("preferredLanguage") & " FROM pagemenus")
                'fillDataListNotPaged(0, dlInterestingLinks, "SELECT ilinkid, slinkimage, slinkurl, slinkdescription_" & Session("preferredLanguage") & ", slinktooltip_" & Session("preferredLanguage") & " FROM interestinglinks")
                'randomizeReview(Session("preferredLanguage"), lblReviewsIntro, lblReviews)
                'randomizeMainPagePhoto(Session("preferredLanguage"), imgHouseMain)

                'If Session("username") <> "Anonymous" Or Session("username") <> "" Then
                '    lblUserDesiredName.Text = Session("desiredName")
                '    lnkLogin.Text = getMessage(Me, "lnkLogout")
                '    lnkReturnToAppMenu.Visible = True
                '    lnkReturnToAppMenu.Text = getMessage(Me, "lnkReturnToAppMenu")
                'End If

                ''firstLogin Redirects drops here
                'If Request.QueryString("reason") = "inactive" Then
                '    lnkLogin.CausesValidation = False
                '    pnlLogin.Visible = True
                '    lblEmailSent.Visible = False
                '    lblWrongUP.Visible = True
                '    lblWrongUP.Text = getMessage(Me, "lblUserInactive")
                '    btnLogin.Focus()
                'ElseIf Request.QueryString("reason") = "unapproved" Then
                '    lnkLogin.CausesValidation = False
                '    pnlLogin.Visible = True
                '    lblEmailSent.Visible = False
                '    lblWrongUP.Visible = True
                '    lblWrongUP.Text = getMessage(Me, "lblUserNotApproved")
                '    btnLogin.Focus()
                'ElseIf Request.QueryString("reason") = "lockedout" Then
                '    lnkLogin.CausesValidation = False
                '    pnlLogin.Visible = True
                '    lblEmailSent.Visible = False
                '    lblWrongUP.Visible = True
                '    lblWrongUP.Text = getMessage(Me, "lblUserLockedOut")
                '    btnLogin.Focus()
                'ElseIf Request.QueryString("reason") = "alreadyloggedin" Then
                '    lnkLogin.CausesValidation = False
                '    pnlLogin.Visible = True
                '    lblEmailSent.Visible = False
                '    lblWrongUP.Visible = True
                '    lblWrongUP.Text = getMessage(Me, "lblAlreadyLoggedIn")
                '    btnLogin.Focus()
                'End If

            End If

        End If

        'If verifyIfUserIsReallyLogged(Me) = False Then

        'lblUserDesiredName.Text = getMessage(Me, "lblUserDesiredName")
        'lnkLogin.Text = getMessage(Me, "lnkLogin")
        'lnkReturnToAppMenu.Visible = False

        'End If

    End Sub


    Private Sub index_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        'If Not Page.IsPostBack And Session("UserIP") = "" Then
        'lblUserDesiredName.ToolTip = findRemoteIP()
        'Session("UserIP") = lblUserDesiredName.ToolTip
        'End If

    End Sub


    <System.Web.Services.WebMethod(), System.Web.Script.Services.ScriptMethod()> Public Shared Function GetSlides() As AjaxControlToolkit.Slide()

        Dim countingHouses As Integer = getSQLQueryAsInteger("SELECT COUNT(*) from processimages")

        Dim imgSlide(countingHouses) As AjaxControlToolkit.Slide

        'Dim luckyNumber1 As Integer
        'Dim limitNumber1 As Integer
        Dim dsImages As Data.DataSet

        'Randomize()

        'limitNumber1 = getSQLQueryAsInteger("SELECT MAX(iimageid) FROM processimages")
        'luckyNumber1 = CInt(Int((limitNumber1 - 1 + 1) * Rnd() + 1))

        'If luckyNumber1 <= 0 Then
        '    luckyNumber1 = 1
        'ElseIf luckyNumber1 > limitNumber1 Then
        '    luckyNumber1 = limitNumber1
        'End If

        dsImages = getSQLQueryAsDataset("SELECT iimageid, simageurl FROM processimages")

        For i = 1 To countingHouses

            imgSlide(i - 1) = New AjaxControlToolkit.Slide(dsImages.Tables(0).Rows(i - 1).Item(1), dsImages.Tables(0).Rows(i - 1).Item(0), "")

        Next i

        Return (imgSlide)

    End Function


    Protected Sub ddLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddLanguage.SelectedIndexChanged

        Session("preferredLanguage") = ddLanguage.SelectedValue
        'fillDataListNotPaged(0, dlMenu, "SELECT ilinkid, slinkimage, slinkurl, slinkdescription_" & Session("preferredLanguage") & ", slinktooltip_" & Session("preferredLanguage") & " FROM pagemenus")
        'fillDataListNotPaged(0, dlInterestingLinks, "SELECT ilinkid, slinkimage, slinkurl, slinkdescription_" & Session("preferredLanguage") & ", slinktooltip_" & Session("preferredLanguage") & " FROM interestinglinks")
        'randomizeReview(Session("preferredLanguage"), lblReviewsIntro, lblReviews)
        'imgHouseMain.ToolTip = getSQLQueryAsString("SELECT simagedescription_" & Session("preferredLanguage") & " FROM houseimages WHERE simageurl = '" & imgHouseMain.ImageUrl & "'")
        setLabels(Me)
        'detectBrowserUsed()

        If Session("preferredLanguage") = "esmx" Then
            pnlSlogan.BackImageUrl = "images/web_08_complete.jpg"
            pnlGarantia.BackImageUrl = "images/web_15.png"
        ElseIf Session("preferredLanguage") = "enus" Then
            pnlSlogan.BackImageUrl = "images/web_08_complete_en.jpg"
            pnlGarantia.BackImageUrl = "images/web_15_en.png"
        End If

    End Sub


    'Private Sub dlIntranetMenu_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlIntranetMenu.ItemCommand

    '    If e.CommandName = "goToMenuLink" Then

    '        Dim strNewMessageIdOrURL As String
    '        strNewMessageIdOrURL = getSQLQueryAsString("SELECT slinkurl FROM pagemenus WHERE ilinkid = '" & dlIntranetMenu.DataKeys(e.Item.ItemIndex) & "'")
    '        If strNewMessageIdOrURL.StartsWith("http") = True Or strNewMessageIdOrURL.EndsWith("aspx") = True Then
    '            Response.Redirect(strNewMessageIdOrURL)
    '        Else
    '            lblInfo.Text = getMessage(Me, strNewMessageIdOrURL)
    '        End If

    '    End If

    'End Sub


    'Private Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click

    '    'Nothing, we are on this page

    'End Sub


    'Private Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click

    '    Response.Redirect("faq.aspx")

    'End Sub


    'Protected Sub lnkLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkLogin.Click

    '    If Session("username") <> "" And Session("username") <> "Anonymous" Then

    '        lnkLogin.CausesValidation = True
    '        lnkReturnToAppMenu.Visible = False
    '        lnkLogin.Text = getMessage(Me, "lnkLogin")
    '        logLogout(Me, Session("username"), Session("UserIP"))
    '        cleanSession(Me)
    '        setLabels(Me)
    '        Exit Sub

    '    End If

    '    If pnlLogin.Visible = True Then
    '        pnlLogin.Visible = False
    '        lnkLogin.CausesValidation = True
    '    Else
    '        lnkLogin.CausesValidation = False
    '        pnlLogin.Visible = True
    '        lblEmailSent.Visible = False
    '        txtUsername.Focus()
    '    End If

    'End Sub


    'Protected Sub lnkReturnToAppMenu_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkReturnToAppMenu.Click

    '    Response.Redirect("main.aspx")

    'End Sub


    'Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click

    '    Dim username As String

    '    txtUsername.Text = txtUsername.Text.Replace("'", "").Replace("\", "").Replace("--", "")
    '    txtPassword.Text = txtPassword.Text.Replace("'", "").Replace("\", "").Replace("--", "")

    '    username = checkLogin(txtUsername.Text, txtPassword.Text)
    '    logLogin(Me, txtUsername.Text, Session("UserIP"), username)

    '    '-4 = User not existent, wrong password
    '    '-3 = Inactive
    '    '-2 = Not approved
    '    '-1 = Locked out
    '    '0 = Already Logged In
    '    'Else = Username

    '    lblEmailSent.Visible = False

    '    If username.Equals("-4") = True Then
    '        lblWrongUP.Visible = True
    '        lblWrongUP.Text = getMessage(Me, "lblWrongUP")
    '        txtPassword.Text = ""
    '        btnLogin.Focus()
    '    ElseIf username.Equals("-3") = True Then
    '        lblWrongUP.Visible = True
    '        lblWrongUP.Text = getMessage(Me, "lblUserInactive")
    '        btnLogin.Focus()
    '    ElseIf username.Equals("-2") = True Then
    '        lblWrongUP.Visible = True
    '        lblWrongUP.Text = getMessage(Me, "lblUserNotApproved")
    '        btnLogin.Focus()
    '    ElseIf username.Equals("-1") = True Then
    '        lblWrongUP.Visible = True
    '        lblWrongUP.Text = getMessage(Me, "lblUserLockedOut")
    '        btnLogin.Focus()
    '    ElseIf username.Equals("0") = True Then
    '        lblWrongUP.Visible = True
    '        lblWrongUP.Text = getMessage(Me, "lblAlreadyLoggedIn")
    '        btnLogin.Focus()
    '    Else
    '        pnlLogin.Visible = False
    '        createUserSessionVariables(Me, username)
    '        Response.Redirect("main.aspx")
    '    End If

    'End Sub


    'Private Sub lblWrongUP3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblWrongUP3.Click

    '    pnlLostPass.Visible = True
    '    pnlLogin.Visible = False
    '    lblErrorEmail.Visible = False
    '    lblErrorAnswer.Visible = False
    '    lblRescueQuestion.Visible = False
    '    lblRescueQuestionData.Visible = False
    '    txtLostPasswordEmail.Enabled = True
    '    reqValLostPassEmail.Enabled = False
    '    btnCancelRescueAttempt.CausesValidation = False

    '    lblEmailSent.Visible = False

    '    txtLostPasswordEmail.Text = ""
    '    btnGetMyRescueQuestion.Text = getMessage(Me, "btnGetMyRescueQuestion")
    '    txtLostPasswordEmail.Focus()

    'End Sub


    'Private Sub btnGetMyRescueQuestion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetMyRescueQuestion.Click

    '    If btnGetMyRescueQuestion.Text = getMessage(Me, "btnGetMyRescueQuestion") Then

    '        txtLostPasswordEmail.Text = txtLostPasswordEmail.Text.Trim
    '        txtLostPasswordEmail.Text = txtLostPasswordEmail.Text.Trim("'", "--")

    '        Dim dsRescue As DataSet
    '        dsRescue = getSQLQueryAsDataset("SELECT suserrescuequestion FROM users WHERE suseremail = '" & txtLostPasswordEmail.Text & "'")

    '        If dsRescue.Tables(0).Rows.Count = 0 Then

    '            lblErrorEmail.Visible = True
    '            lblErrorEmail.Text = getMessage(Me, "UserDoesntExistsError")
    '            txtLostPasswordEmail.Focus()
    '            Exit Sub

    '        Else

    '            lblErrorEmail.Visible = False
    '            txtLostPasswordEmail.Enabled = False
    '            lblRescueQuestion.Visible = True
    '            lblRescueQuestionData.Visible = True
    '            lblRescueAnswer.Visible = True
    '            txtRescueAnswer.Visible = True
    '            txtRescueAnswer.Text = ""
    '            lblRescueQuestionData.Text = dsRescue.Tables(0).Rows(0).Item(0)
    '            btnGetMyRescueQuestion.Text = getMessage(Me, "btnGetMyRescueAnswer")
    '            reqValLostPassEmail.Enabled = True
    '            txtRescueAnswer.Focus()

    '        End If


    '    Else

    '        Dim dsRescue As DataSet
    '        dsRescue = getSQLQueryAsDataset("SELECT suserrescueanswer, suseremail FROM users WHERE suseremail = '" & txtLostPasswordEmail.Text & "'")
    '        If logRescueAttempt(Me, dsRescue.Tables(0).Rows(0).Item("suseremail"), Session("UserIP"), dsRescue.Tables(0).Rows(0).Item("suserrescueanswer"), txtRescueAnswer.Text) = True Then

    '            txtLostPasswordEmail.Text = ""
    '            lblErrorEmail.Visible = False
    '            lblErrorAnswer.Visible = False
    '            lblRescueQuestion.Visible = False
    '            lblRescueQuestionData.Visible = False
    '            lblRescueAnswer.Visible = False
    '            txtRescueAnswer.Visible = False
    '            txtRescueAnswer.Text = ""

    '            reqValLostPassEmail.Enabled = True
    '            reqValRescueAnswer.Enabled = False

    '            pnlLostPass.Visible = False
    '            pnlLogin.Visible = True
    '            lblEmailSent.Visible = True
    '            lblEmailSent.Text = getMessage(Me, "lblEmailSent") & dsRescue.Tables(0).Rows(0).Item("suseremail")
    '            Exit Sub

    '        Else

    '            lblErrorAnswer.Visible = True
    '            lblErrorAnswer.Text = getMessage(Me, "AnswerError")
    '            txtRescueAnswer.Focus()
    '            Exit Sub

    '        End If

    '    End If

    'End Sub


    'Private Sub btnCancelRescueAttempt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRescueAttempt.Click

    '    reqValLostPassEmail.Enabled = False
    '    reqValRescueAnswer.Enabled = False

    '    pnlLostPass.Visible = False
    '    pnlLogin.Visible = True

    '    txtLostPasswordEmail.Text = ""
    '    lblErrorEmail.Visible = False
    '    lblErrorAnswer.Visible = False
    '    lblRescueQuestion.Visible = False
    '    lblRescueQuestionData.Visible = False
    '    lblRescueAnswer.Visible = False
    '    txtRescueAnswer.Visible = False
    '    txtRescueAnswer.Text = ""

    '    reqValLostPassEmail.Enabled = True
    '    reqValRescueAnswer.Enabled = False

    'End Sub


End Class
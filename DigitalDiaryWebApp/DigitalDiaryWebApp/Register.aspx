<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DigitalDiaryWebApp.Register" %>

<!DOCTYPE HTML>
<html>

<head>
  <title>Digital Diary</title>
  <meta name="description" content="website description" />
  <meta name="keywords" content="website keywords, website keywords" />
  <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
  <link rel="stylesheet" type="text/css" href="style/style.css" title="style" />
</head>

<body>
<form id="formDiary" runat="server">
  <div id="main">
    <div id="header">
      <div id="logo">
        <div id="logo_text">
          <!-- class="logo_colour", allows you to change the colour of the text -->
          <h1>Digital Diary</h1>
          <h2>User friendly. Contemporary. Write your thoughts.</h2>
        </div>
      </div>

      <div id="menubar">
        
      </div>
   </div>

    <div id="site_content">
            <table>
                <tr>                              
                    <td>
                         <asp:Table runat="server">
                             <asp:TableRow>
                                 <asp:TableCell ColumnSpan="2"><h2>Sign up</h2></asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                 <asp:TableCell>Email: </asp:TableCell>
                                 <asp:TableCell><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                 <asp:TableCell>Username: </asp:TableCell>
                                 <asp:TableCell><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                 <asp:TableCell>Password:</asp:TableCell>
                                 <asp:TableCell><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                 <asp:TableCell>Fullname: </asp:TableCell>
                                 <asp:TableCell><asp:TextBox ID="txtFullname" runat="server"></asp:TextBox></asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                 <asp:TableCell ColumnSpan="2"><asp:Button ID="btnRegister" runat="server" Text="Register" Width="100%" CssClass="myButtons" OnClick="btnRegister_Click" /></asp:TableCell>
                             </asp:TableRow>
                         </asp:Table>
                    </td>
                    <td>
                         <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>     
                    </td>
                 </tr>
            </table>
    </div>
    <div id="footer">      
        <table style="width: 100%">
         <tr>
            <td colspan="2"><h2>Contact us</h2></td>            
        </tr>
            
        <tr>
            <td>Address: 430B Yishun Avenue 11, Singapore, 762430</td>
            <td><a href="https://uk.linkedin.com/in/freddyjilson" target="_blank">LinkedIn</a></td>
        </tr>
        <tr>
            <td>Email: jilsonfreddy@hotmail.com</td>
            <td><a href="https://github.com/FreddyJilson" target="_blank">GitHub</a></td>
        </tr>
        <tr>
            <td>Contact: +6581725085</td>
            <td><a href="https://github.com/FreddyJilson/DigitalDiary" target="_blank">Source code</a></td>
        </tr>
        </table>
   </div>
  </div>
</form>
</body>
</html>

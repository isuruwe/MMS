<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>ASP.NET FullCalendar</title>
    <link href="~/Scripts/js/jquery-ui.min.css" rel="stylesheet" />
<link href="~/Scripts/js/fullcalendar.min.css" rel="stylesheet" />
<link href="~/Scripts/js/jquery.qtip.min.css" rel="stylesheet" />
   <link rel="stylesheet" href="~/Scripts/css/bootstrap.min.css">
<link rel="stylesheet" href="Scripts/css/bootstrap.min.css">
<link rel="stylesheet" href="Scripts/css/animate.min.css">
<link rel="stylesheet" href="Scripts/css/et-line-font.css">
<link rel="stylesheet" href="Scripts/css/nivo-lightbox.css">
  <link rel="stylesheet" href="~/Scripts/css/style.css">
      <script src="Scripts/js/jquery.js"></script>
<script src="Scripts/js/bootstrap.min.js"></script>
<script src="Scripts/js/smoothscroll.js"></script>
<script src="Scripts/js/isotope.js"></script>
<script src="Scripts/js/imagesloaded.min.js"></script>
<script src="Scripts/js/nivo-lightbox.min.js"></script>
<script src="Scripts/js/jquery.backstretch.min.js"></script>
<script src="Scripts/js/wow.min.js"></script>
<script src="Scripts/js/custom.js"></script>
<link rel="stylesheet" href="Scripts/css/style.css">
    <style type='text/css'>
        body
        {
            margin-top: 40px;
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande" ,Helvetica,Arial,Verdana,sans-serif;
        }
        #calendar
        {
            width: 900px;
            margin: 0 auto;
        }
        /* css for timepicker */
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .style1
        {
            width: 100%;
        }
        
        /* table fields alignment*/
        .alignRight
        {
        	text-align:right;
        	padding-right:10px;
        	padding-bottom:10px;
        }
        .alignLeft
        {
        	text-align:left;
        	padding-bottom:10px;
        }
    </style>
</head>
<body data-spy="scroll"  style="background-color:#dbe4eb" data-offset="50">
    
     <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
         
            <div class="navbar-collapse collapse">

               
            </div>
        </div>
    </div>
 
    <section class="navbar navbar-fixed-top custom-navbar" role="navigation">
                <div class="container">
                   
                    <div class="collapse navbar-collapse">
                        <ul class="nav navbar-nav navbar-left">
                            <li>
                                <a href="Home/Index">HOME</a>
                            </li>
                        </ul>
                        
                      
                    </div>




                </div>
            </section>
    <section id="about">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="calendar">
    </div>
    <div id="updatedialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px;display: none;"
        title="Update or Delete Event">
        <table class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" size="33" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
               <tr>
                <td class="alignRight">
                    Clinic:</td>
                <td class="alignLeft">
                    <select id="upSelect1">
                      <option value="1">PAEDIATRIC CLINIC</option>
                         <option value="2">GYN & OBS CLINIC (ANTENATAL)</option>
                         <option value="3">MEDICAL CLINIC</option>
                         <option value="4">NEUROLOGY CLINIC</option>
                         <option value="5">CARDIOLOGY CLINIC</option>
                         <option value="6">EYE CLINIC</option>
                        <option value="7">DERMATOLOGY CLINIC</option>
                        <option value="8">ORTHOPAEDIC CLINC</option>
                        <option value="9">ENT CLINIC</option>
                        <option value="10">FAMILIY PHYSICIAN</option>
                        <option value="11">RADIOLOGY CLINIC</option>
                          <option value="12">UROLOGY CLINIC</option>
                          <option value="13">SURGICAL CLINIC</option>
                          <option value="14">DIETICIAN CLINIC</option>
                          <option value="15">PAIN CLINIC</option>
                        <option value="16">PSYCHIATRIC CLINIC</option>
                        <option value="17">ONCOLOGY CLINIC</option>
                        <option value="18">NEURO SURGICAL CLINIC</option>
                          <option value="19">HEMATOLOGY CLINIC</option>
                    </select>  </td>
            </tr>
            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="eventStart"></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    End: </td>
                <td class="alignLeft">
                    <span id="eventEnd"></span><input type="hidden" id="eventId" /></td>
            </tr>
        </table>
    </div>
    <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px;" title="Add Event">
    <table class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="33" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
        <tr>
                <td class="alignRight">
                    Clinic:</td>
                <td class="alignLeft">
                    <select id="Select1">
                        <option value="1">PAEDIATRIC CLINIC</option>
                         <option value="2">GYN & OBS CLINIC (ANTENATAL)</option>
                         <option value="3">MEDICAL CLINIC</option>
                         <option value="4">NEUROLOGY CLINIC</option>
                         <option value="5">CARDIOLOGY CLINIC</option>
                         <option value="6">EYE CLINIC</option>
                        <option value="7">DERMATOLOGY CLINIC</option>
                        <option value="8">ORTHOPAEDIC CLINC</option>
                        <option value="9">ENT CLINIC</option>
                        <option value="10">FAMILIY PHYSICIAN</option>
                        <option value="11">RADIOLOGY CLINIC</option>
                          <option value="12">UROLOGY CLINIC</option>
                          <option value="13">SURGICAL CLINIC</option>
                          <option value="14">DIETICIAN CLINIC</option>
                          <option value="15">PAIN CLINIC</option>
                        <option value="16">PSYCHIATRIC CLINIC</option>
                        <option value="17">ONCOLOGY CLINIC</option>
                        <option value="18">NEURO SURGICAL CLINIC</option>
                          <option value="19">HEMATOLOGY CLINIC</option>
                    </select>  </td>
            </tr>

            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="addEventStartDate" ></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    End:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate" ></span></td>
            </tr>
        </table>
        
    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    </form>
   </section>
 <script src="/Scripts/js/moment.min.js"></script>
      <script src="/Scripts/js/jquery.min.js"></script>
    <script src="/Scripts/js/jquery-ui.min.js"></script>
    <script src="/Scripts/js/jquery.qtip.min.js"></script>
    <script src="/Scripts/js/fullcalendar.min.js"></script>
    <script src="/Scripts/js/calendarscript.js" type="text/javascript"></script>
   
</body>
</html>

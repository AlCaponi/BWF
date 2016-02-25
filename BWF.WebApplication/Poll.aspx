<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Poll.aspx.cs" Inherits="Poll" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		
		<title>ZSO Emme - Umfrage neue Soldaten</title>
		
		<script type="text/javascript" src="js/jquery.js"></script>
		<script type="text/javascript" src="js/bootstrap.min.js"></script>
        <script type="text/javascript" src="js/default.js"></script>
        <script type="text/javascript" src="js/bootstrap-slider.min.js">    </script>
        <script type="text/javascript" src="js/validator.js"></script>
        <!-- <script src="assets/js/jquery-ui-1.10.0.custom.min.js" type="text/javascript"></script> -->
		
		<link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="css/bootstrap-theme.css" />
        <link rel="stylesheet" type="text/css" href="css/custom-theme/jquery-ui-1.10.0.custom.css" />
        <link rel="stylesheet" type="text/css" href="assets/css/font-awesome.min.css" />
        <link rel="Stylesheet" type="text/css" href="css/bootstrap-slider.min.css" />
        <!--[if IE 7]>
        <link rel="stylesheet" href="assets/css/font-awesome-ie7.min.css">
        <![endif]-->
        <!--[if lt IE 9]>
        <link rel="stylesheet" type="text/css" href="css/custom-theme/jquery.ui.1.10.0.ie.css"/>
        <![endif]-->
        <!-- <link href="assets/css/docs.css" rel="stylesheet" /> -->
        <!-- <link href="assets/js/google-code-prettify/prettify.css" rel="stylesheet" /> -->

        <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
        <!--[if lt IE 9]>
        <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->
		<link rel="stylesheet" type="text/css" href="css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container poll-form">
				<form runat="server" method="post" id="frmPoll" data-toggle="validator">
                    <div class="img-responsive">
                        <img src="images/Logo_ZSOEMME.png" alt="Logo" class="img-responsive" />
                    </div>
				    <div class="progress">
                        <div id="pbStatus" class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                            <!-- <span class="sr-only"></span> -->
                        </div>
                    </div>
                    <%foreach (BWF.Library.FragenGruppe fragenGruppe in FragenGruppeCol)
                      { %>
                          <div class="slide" id='slide<%= fragenGruppe.Sort %>'>
                            <h2><%= fragenGruppe.Name %></h2>
                            <%foreach (BWF.Library.Fragen fragen in fragenGruppe.FragenCol)
                              {%>
                                <div class="row question" data-question-id="<%=fragen.ID %>" data-question-type="<%=fragen.FragenTypID %>">
                                    <div class="form-group col-md-12">
                                        <label><%= fragen.Frage%></label>
                                        <br />
                                        <%switch (fragen.FragenTypID.ToString()) 
                                          {
                                              //Frgae mit Multiline Text
                                              case "707b446e-4c25-4494-a18a-f444480c6e25": 
                                                    {
                                                        foreach (BWF.Library.Antwort antwort in fragen.AntwortCol) 
                                                        { 
                                                            %>
                                                            <textarea rows="3" class="span6" placeholder="<%=antwort.MöglicheAntwort %>" style="width:100%;"></textarea>
                                                            <%
                                                        }
                                                      break;
                                                    }
                                              // Frage mit Single Line Text
                                              case "65bbbf50-21c0-431b-9f5e-66629c04e2a6":
                                                    {
                                                        foreach (BWF.Library.Antwort antwort in fragen.AntwortCol)
                                                        {
                                                            if (antwort.Zusatz == null)
                                                            {
                                                                if (fragen.ValidationType == "phone")
                                                                {
                                                                    %>
                                                                    <input type="text" pattern="^\s*\+?\s*([0-9][\s-]*){9,}$"  class="form-control" placeholder="<%= antwort.MöglicheAntwort %>" <%= fragen.Required ? "required" : "" %> oninvalid="setCustomValidity('Ihre Eingabe muss eine gültige Telefonnummer sein.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                                                    <div class="help-block with-errors"></div>
                                                                    <% 
                                                                }
                                                                else
                                                                {
                                                                    %>
                                                                    <input type="<%= fragen.ValidationType %>"  class="form-control" placeholder="<%= antwort.MöglicheAntwort %>" <%= fragen.Required ? "required" : "" %> />
                                                                    <div class="help-block with-errors"></div>
                                                                    <% 
                                                                }
                                                                
                                                            }
                                                            else 
                                                            {
                                                                %>
                                                                    <label><input type="checkbox" /><%= antwort.Zusatz %></label>
                                                                <%
                                                            }
                                                            
                                                        }
                                                        break;
                                                    }
                                              //Frage mit Skala     
                                              case "9c4c7610-fffc-4159-af45-0880f010936c":
                                                    {
                                                        %>
                                                        <div id="slider" class="zso_slider" data-min-value="<%=fragen.AntwortCol[0].MöglicheAntwort %>" data-max-value="<%=fragen.AntwortCol[1].MöglicheAntwort %>" data-slider-value="<%=fragen.AntwortCol[2].MöglicheAntwort %>" >&nbsp;</div><br />
                                                        <%
                                                        break;
                                                    }
                                               //Frage mit multiple Choice
                                              case "e0285169-1340-4b69-8411-e7272017a28a":
                                                    {
                                                        foreach (BWF.Library.Antwort antwort in fragen.AntwortCol)
                                                        {
                                                            if (antwort.Zusatz == null)
                                                            {
                                                                %>
                                                                    <label><input data-antwort-id="<%=antwort.ID %>" type="checkbox" /> <%= antwort.MöglicheAntwort %></label><br />
                                                                <% 
                                                            }
                                                            else 
                                                            {
                                                                %>
                                                                    <label><input data-antwort-id="<%=antwort.ID %>" type="checkbox" id="zusatzCheckbox" onclick="$('#<%=antwort.ID %>').toggle();"/> <%=antwort.Zusatz %></label><input id="<%=antwort.ID %>" type="text" class="form-control" style="display:none" />
                                                                <%
                                                            }
                                                            
                                                        }
                                                        break;
                                                    }
                                              //Frage mit Single Choice
                                              case "d1609e72-54a2-4680-bc8b-02bd0ebaecf3":
                                                    {
                                                        foreach (BWF.Library.Antwort antwort in fragen.AntwortCol)
                                                        { 
                                                            %>
                                                            <label><input type="radio" name="<%=fragen.ID %>" data-fragen-id="<%=fragen.ID %>" data-fragen-toggle="<%=antwort.Zusatz != null %>" data-antwort-id="<%=antwort.ID %>" <%= fragen.Required ? "required" : "" %>/> <%=antwort.MöglicheAntwort %></label><br />
                                                            <%
                                                            if (antwort.Zusatz != null)
                                                            { 
                                                                %>
                                                                <div id='<%=fragen.ID+"show" %>' style="display:none;">
                                                                    <label><%=antwort.Zusatz %></label><input id="<%=antwort.ID %>" name="<%=antwort.ID %>" type="text" class="form-control" required />
                                                                </div>
                                                                <%
                                                            }
                                                        }
                                                        %>
                                                        <div class="help-block with-errors"></div>
                                                        <%
                                                        break;
                                                    }
                                               //Frage mit Dropdown
                                              case "c6a0db90-0fe5-4657-afde-450081f7ae9b":
                                                    {
                                                        %>
                                                        <select class="form-control">
                                                            <option value="">Bitte wählen...</option>
                                                        <%
                                                            
                                                        foreach (BWF.Library.Antwort antwort in fragen.AntwortCol)
                                                        { 
                                                            %>
						                                        <option data-antwort-id="<%=antwort.ID %>" value="<%=antwort.ID %>"><%=antwort.MöglicheAntwort %></option>
                                                            <%
                                                        }
                                                        %>
                                                            </select>
                                                        <%
                                                        break;
                                                    }
                                          } %>
                                          <label class="label label-info"><%=fragen.Hinweis %></label>
                                    </div>
                                </div>
                            <%} %>
                                <div class="form-group">
                                <%if (fragenGruppe.Sort != 1)
                                  { %>
                                    <input type="button" value="&lt;&lt; Zurück" class="btn btn-default slide-prev" data-perc="<%= GetPreviousPercentage(fragenGruppe.Sort) %>" data-slide="<%= fragenGruppe.Sort - 1 %>" />
                                <%}
                                  if (fragenGruppe.Sort != FragenGruppeCol.Count)
                                  { %>                                
	    					        <input type="button" id="btnNext" value="Weiter &gt;&gt;" class="btn btn-primary slide-next" data-perc="<%= GetNextPercentage(fragenGruppe.Sort) %>" data-slide='<%= fragenGruppe.Sort + 1 %>' />
                                <%}
                                  if (fragenGruppe.Sort == FragenGruppeCol.Count)
                                  {
                                  %>
                                    <asp:HiddenField runat="server" ID="hidAnswerValues" ClientIDMode="Static" Value="" />
                                    <asp:Button runat="server" UseSubmitBehavior="false" ID="btnSubmit" ClientIDMode="static" class="btn btn-primary slide-next" OnClick="btnSubmit_OnClick" Text="Einsenden" OnClientClick="$('#hidAnswerValues').val(createJson());if(!checkValid())return false;" type="submit"/>
                                  <%
                                  }
                                  %>

					            </div>
                          </div>
                    <%} %>
				</form>
                <script>
                    function checkValid() {
                        $('#frmPoll').validator('validate');
                        var hasErrors = false;
                        $('.with-errors').each(function () {
                            if (!$(this).is(':empty')) {
                                hasErrors = true;
                            }
                        });
                        if (hasErrors) {
                            return false;
                        }
                        return true;
                    }
                </script>
			</div>
		</div>
	</body>
</html>

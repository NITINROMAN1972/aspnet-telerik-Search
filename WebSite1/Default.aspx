<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableViewState="true" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<%--<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap1.min.css" rel="stylesheet" />

    <!-- Popper.js -->
    <script src="js/popper.min.js"></script>
    <script src="js/popper1.min.js"></script>

    <!-- jQuery -->
    <script src="js/jquery-3.6.0.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery-3.3.1.slim.min.js"></script>

    <!-- Bootstrap JS -->
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/bootstrap1.min.js"></script>

    <!-- Select2 library CSS and JS -->
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <!-- Bootstrap Selectpicker CSS and JS -->
    <link href="css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="js/bootstrap-select.min.js"></script>

    <style>
        .custom-width {
    width: 97%; /* Adjust the percentage as needed */
     /* You can set a maximum width if necessary */
}

    </style>

    <script>
        $(document).ready(function () {
            // To style only selects with the my-select class
            $('.selectpicker select').selectpicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                setTimeout(function () {
                    $('.selectpicker select').selectpicker('refresh');
                }, 0);
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="mt-4 mx-auto col-md-11">
            <div class="fw-semibold fs-5 text-dark">
                <asp:Literal ID="lit" Text="Telerik Reporting" runat="server"><h3>Telerik Reporting</h3></asp:Literal>
            </div>
        </div>

        <!-- Main Control Start -->
        <div class="card mt-2 shadow p-3 mb-5 bg-body rounded mx-auto custom-width">
            <div class="card-body">

                <div class="row mt-3 mb-2 d-flex justify-content-around">
                    <div class="col-md-3 ml-2">
                        <label for="ddlCountry" class="col-form-label s-12">Country</label>
                        <asp:DropDownList ID="ddlCountry" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Class="form-control selectpicker select-search-custom" runat="server" data-live-search="true" data-size="5" data-width="120%" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 ml-2">
                        <label for="ddlState" class="col-form-label s-12">State</label>
                        <asp:DropDownList ID="ddlState" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control selectpicker select-search-custom" runat="server" data-live-search="true" data-size="5" data-width="120%" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 ml-2">
                        <label for="ddlCity" class="col-form-label s-12">City</label>
                        <asp:DropDownList ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" CssClass="form-control selectpicker select-search-custom" runat="server" data-live-search="true" data-size="5" data-width="120%" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

                <div class="row mt-2 mb-3 d-flex justify-content-around">
                    <div class="col-md-3 ml-2">
                        <label for="txtFromDate" class="col-form-label s-12">From Date (Pervious Hearing Date)</label>
                        <input type="date" id="txtFromDate" runat="server" class="form-control" style="width:120%" />
                    </div>
                    <div class="col-md-3 ml-2">
                        <label for="txtToDate" class="col-form-label s-12">To Date</label>
                        <input type="date" id="txtToDate" runat="server" class="form-control" style="width:120%" />
                    </div>
                    <div class="col-md-3 mt-3 ml-2 d-flex align-items-end">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>

            <!-- Telerik Grid -->
            <div id="telerik" runat="server" visible="true">
                <telerik:RadGrid ID="Radgrid1" runat="server" Visible="false" PageSize="30" ViewStateMode="Enabled" OnNeedDataSource="Radgrid1_NeedDataSource" Skin="Office2007" CssClass="borderLessDialog" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true" ShowGroupPanel="true" ShowFooter="true" AutoGenerateColumns="false" border-spacing="false" AllowMultiColumnSorting="true" CellPadding="5">

                    <GroupingSettings GroupByFieldsSeparator="" ShowUnGroupButton="True"></GroupingSettings>

                    <MasterTableView ClientDataKeyNames="EmployeeID" EnableHeaderContextMenu="True" EnableHeaderContextFilterMenu="True" AllowAutomaticInserts="True" ShowGroupFooter="True" CommandItemDisplay="TopAndBottom" HeaderStyle-Width="150px">
                        <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToExcelButton="True" ShowExportToPdfButton="True" ShowExportToCsvButton="True"></CommandItemSettings>
                        <Columns>
                            <telerik:GridBoundColumn DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" Display="False" UniqueName="EmployeeID" FilterControlAltText="Filter EmployeeID column"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>

                    <HeaderStyle VerticalAlign="Top" BorderColor="#9eb6ce" BorderStyle="Groove"  />

                    <MasterTableView CommandItemDisplay="TopAndBottom" ShowGroupFooter="true" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                        ClientDataKeyNames="EmployeeID" AllowAutomaticInserts="true" AlternatingItemStyle-BackColor="Lavender">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowExportToPdfButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px" Display="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FirstName" HeaderText="Employee First Name" SortExpression="EmployeeFirstName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="LastName" HeaderText="EmployeeLastName" SortExpression="EmployeeLastName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CountryName" HeaderText="Country" SortExpression="Country" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StateName" HeaderText="State" SortExpression="State" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CityName" HeaderText="City" SortExpression="City" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BirthDate" HeaderText="BirthDate" SortExpression="BirthDate" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="JobTitle" HeaderText="JobTitle" SortExpression="JobTitle" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Salary" HeaderText="Salary" SortExpression="Salary" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Date" HeaderText="Date" SortExpression="Date" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        
                            
                        </Columns>
                    </MasterTableView>

                    <PagerStyle AlwaysVisible="true" Mode="NextPrevNumericAndAdvanced"></PagerStyle>

                    <ExportSettings ExportOnlyData="true" IgnorePaging="true" Excel-Format="ExcelML" OpenInNewWindow="true">
                        <Pdf PageWidth="1500px" PaperSize="A4" DefaultFontFamily="Arial" BorderStyle="Thin" BorderType="AllBorders"></Pdf>
                    </ExportSettings>

                    <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true">
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>

                </telerik:RadGrid>
            </div>

        </div>
    </form>

</body>
</html>

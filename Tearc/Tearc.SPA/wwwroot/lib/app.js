﻿$ = jQuery = require( "jquery" ); // Add jQuery to global window for Bootstrap and SignalR.
require( "bootstrap" );
require( "signalr" );
React = require( "react" );
ReactDOM = require("react-dom");

// ChartJS.
LineChart = require("react-chartjs").Line;
BarChart = require("react-chartjs").Bar;
DoughnutChart = require("react-chartjs").Doughnut;

// Material-UI components.
MuiThemeProvider = require("material-ui/styles/MuiThemeProvider").default;
AppBar = require("material-ui/AppBar").default;
GridList = require("material-ui/GridList").default;
GridTile = require("material-ui/GridList").GridTile;
baseTheme = require("material-ui/styles/baseThemes/lightBaseTheme").default;
getMuiTheme = require("material-ui/styles/getMuiTheme").default;
AutoComplete = require("material-ui/AutoComplete").default;
Card = require("material-ui/Card").Card;
CardActions = require("material-ui/Card").CardActions;
CardHeader = require("material-ui/Card").CardHeader;
CardText = require("material-ui/Card").CardText;
Checkbox = require( "material-ui/Checkbox" ).default;
Chip = require("material-ui/Chip").default;
Dialog = require("material-ui/Dialog").default;
IconButton = require( "material-ui/IconButton" ).default;
Subheader = require( "material-ui/Subheader" ).default;
StarBorder = require("material-ui/svg-icons/toggle/star-border").default;
FlatButton = require( "material-ui/FlatButton" ).default;
Drawer = require("material-ui/Drawer").default;
MenuItem = require( "material-ui/MenuItem" ).default;
Paper = require("material-ui/Paper").default;
RadioButtonGroup = require( "material-ui/RadioButton" ).RadioButtonGroup;
RadioButton = require("material-ui/RadioButton").RadioButton;
RaisedButton = require( "material-ui/RaisedButton" ).default;
SelectField = require("material-ui/SelectField").default;
Snackbar = require("material-ui/Snackbar").default;
Step = require( "material-ui/Stepper" ).Step;
Stepper = require("material-ui/Stepper").Stepper;
StepLabel = require( "material-ui/Stepper" ).StepLabel;
TextField = require( "material-ui/TextField" ).default;
Toggle = require("material-ui/Toggle").default;
Table = require("material-ui/Table").Table;
TableBody = require("material-ui/Table").TableBody;
TableHeader = require("material-ui/Table").TableHeader;
TableHeaderColumn = require("material-ui/Table").TableHeaderColumn;
TableRow = require("material-ui/Table").TableRow;
TableRowColumn = require("material-ui/Table").TableRowColumn;

// Material-UI icons.
IconDelete = require("material-ui/svg-icons/action/delete").default;
IconEdit = require("material-ui/svg-icons/content/create").default;
IconFilter = require("material-ui/svg-icons/content/filter-list").default;
IconSearch = require("material-ui/svg-icons/action/search").default;
IconMovie = require("material-ui/svg-icons/AV/movie").default;
IconPhone = require("material-ui/svg-icons/communication/phone").default;

// Used by Material-UI.
injectTapEventPlugin = require( "react-tap-event-plugin" );
injectTapEventPlugin();

"use strict";

var RouteLink = dotnetify.react.router.RouteLink;

var Index = React.createClass({
    displayName: "Index",

    getInitialState: function getInitialState() {
        var _this = this;

        // Connect this component to the back-end view model.
        this.vm = dotnetify.react.connect("IndexVM", this);
        this.vm.onRouteEnter = function (path, template) {
            return template.Target = "Content";
        };
        // Functions to dispatch state to the back-end.
        this.dispatch = function (state) {
            _this.vm.$dispatch(state);
            _this.gridview_VM.$dispatch(state);
        };

        var state = window.vmStates.IndexVM || {};
        state.selectedLink = "";
        state.open = false;
        return state;
    },

    render: function render() {
        var _this2 = this;

        var styles = {
            navMenu: { padding: "15px", color: "rgb(125,135,140)", backgroundColor: "rgb(255,255,255)" },
            link: { color: "#b8c7ce" },
            exampleLink: { color: "#b8c7ce", display: "block", padding: "5px 0px 5px 20px", textDecoration: "none" },
            activeExampleLink: { color: "black", backgroundColor: "#eef0f0", display: "block", padding: "5px 0px 5px 20px", textDecoration: "none" },
            desc: { padding: "0 15px 0 35px", fontSize: "9pt", marginBottom: ".5em" },
            header: { fontSize: "24px", color: "rgba(0, 188, 212, 1)" },
            list: { paddingLeft: "0", listStyleType: "none", margin: "0 -15px" },
            listItem: { paddingLeft: "20px" },
            bullet: { color: "rgba(255,205,0,.8)", transform: "scale(.5)" },
            copyright: { color: "rgb(125,135,140)", fontSize: "8pt" },
            hide: { display: "none" },
            root: {
                display: 'flex',
                flexWrap: 'wrap',
                justifyContent: 'space-around'
            },
            gridList: {
                width: 500,
                height: 450,
                overflowY: 'auto'
            }
        };

        var tilesData = [{
            img: 'http://www.material-ui.com/images/grid-list/00-52-29-429_640.jpg',
            title: 'Breakfast',
            author: 'jill111'
        }, {
            img: 'http://www.material-ui.com/images/grid-list/burger-827309_640.jpg',
            title: 'Tasty burger',
            author: 'pashminu'
        }, {
            img: 'http://www.material-ui.com/images/grid-list/camera-813814_640.jpg',
            title: 'Camera',
            author: 'Danson67'
        }, {
            img: 'http://www.material-ui.com/images/grid-list/morning-819362_640.jpg',
            title: 'Morning',
            author: 'fancycrave1'
        }, {
            img: 'http://www.material-ui.com/images/grid-list/hats-829509_640.jpg',
            title: 'Hats',
            author: 'Hans'
        }, {
            img: 'http://www.material-ui.com/images/grid-list/honey-823614_640.jpg',
            title: 'Honey',
            author: 'fancycravel'
        }, {
            img: 'http://www.material-ui.com/images/grid-list/vegetables-790022_640.jpg',
            title: 'Vegetables',
            author: 'jill111'
        }, {
            img: 'http://www.material-ui.com/images/grid-list/water-plant-821293_640.jpg',
            title: 'Water plant',
            author: 'BkrmadtyaKarki'
        }];

        var GridListExampleSimple = function GridListExampleSimple() {
            return React.createElement(
                "div",
                { style: styles.root },
                React.createElement(
                    GridList,
                    {
                        cellHeight: 180,
                        style: styles.gridList
                    },
                    React.createElement(
                        Subheader,
                        null,
                        "December"
                    ),
                    tilesData.map(function (tile) {
                        return React.createElement(
                            GridTile,
                            {
                                key: tile.img,
                                title: tile.title,
                                subtitle: React.createElement(
                                    "span",
                                    null,
                                    "by ",
                                    React.createElement(
                                        "b",
                                        null,
                                        tile.author
                                    )
                                ),
                                actionIcon: React.createElement(
                                    IconButton,
                                    null,
                                    React.createElement(StarBorder, { color: "white" })
                                )
                            },
                            React.createElement("img", { src: tile.img })
                        );
                    })
                )
            );
        };

        var setSelectedLink = function setSelectedLink(linkId) {
            return _this2.setState({ selectedLink: linkId });
        };

        var showLinks = function showLinks(links) {
            return links.map(function (link) {
                return React.createElement(
                    "li",
                    { key: link.Id },
                    React.createElement(
                        RouteLink,
                        { vm: _this2.vm, route: link.Route,
                            style: link.Id == _this2.state.selectedLink ? styles.activeExampleLink : styles.exampleLink,
                            className: "example-link",
                            onClick: function () {
                                return setSelectedLink(link.Id);
                            } },
                        React.createElement("span", { style: styles.bullet, className: "glyphicon glyphicon-asterisk" }),
                        link.Caption
                    ),
                    React.createElement(
                        "div",
                        { style: styles.desc },
                        link.Description
                    )
                );
            });
        };

        var handleToggle = function handleToggle() {
            _this2.setState({ open: !_this2.state.open });
        };

        var handleClose = function handleClose() {
            return _this2.setState({ open: false });
        };

        return React.createElement(
            "div",
            null,
            React.createElement(
                MuiThemeProvider,
                null,
                React.createElement(
                    "div",
                    { classNames: "well", id: "pageHeader" },
                    React.createElement(
                        "div",
                        { className: "row" },
                        React.createElement(
                            "div",
                            { className: "col-md-12" },
                            React.createElement(AppBar, { style: { marginBottom: "1em" },
                                title: this.state.LocalizedStrings.Title,
                                onLeftIconButtonTouchTap: handleToggle,
                                iconElementRight: React.createElement(LanguageToggle, { onToggle: function (code) {
                                        return _this2.dispatch({ CultureCode: code });
                                    } }) })
                        )
                    )
                )
            ),
            React.createElement(
                MuiThemeProvider,
                null,
                React.createElement(
                    "div",
                    { classNames: "container-fluid" },
                    React.createElement(
                        Drawer,
                        {
                            docked: false,
                            width: 200,
                            open: this.state.open,
                            onRequestChange: function (open) {
                                return _this2.setState({ open: open });
                            }
                        },
                        React.createElement(
                            "div",
                            { style: styles.navMenu },
                            React.createElement(
                                "div",
                                null,
                                React.createElement(
                                    "h3",
                                    { style: styles.header },
                                    this.state.LocalizedStrings.ProjectNav
                                ),
                                React.createElement(
                                    "ul",
                                    { id: "BasicExamples", style: styles.list },
                                    showLinks(this.state.BasicExampleLinks),
                                    showLinks(this.state.FurtherExampleLinks)
                                )
                            ),
                            React.createElement(
                                "div",
                                { style: styles.copyright },
                                React.createElement("br", null),
                                React.createElement(
                                    "small",
                                    null,
                                    "© 2017 Long Nguyen."
                                ),
                                React.createElement("br", null),
                                React.createElement("br", null)
                            )
                        )
                    ),
                    React.createElement(
                        "div",
                        { id: "Content" },
                        GridListExampleSimple
                    )
                )
            )
        );
    }
});

var LanguageToggle = React.createClass({
    displayName: "LanguageToggle",

    getInitialState: function getInitialState() {
        return {
            code: "en-US",
            language: "English"
        };
    },
    render: function render() {
        var _this3 = this;

        var handleToggle = function handleToggle(event, checked) {
            var code = !checked ? "en-US" : "fr-FR";
            _this3.setState({ code: code });
            _this3.setState({ language: !checked ? "English" : "Français" });
            _this3.props.onToggle(code);
        };

        return React.createElement(Toggle, { style: { marginTop: "1em", width: "7em" },
            trackSwitchedStyle: { backgroundColor: "#e0e0e0" },
            thumbSwitchedStyle: { backgroundColor: "#11cde5" },
            onToggle: handleToggle,
            label: this.state.language,
            labelStyle: { color: "white", fontSize: "medium" }
        });
    }
});

ReactDOM.render(React.createElement(Index, null), document.querySelector("body"));


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
            return _this.vm.$dispatch(state);
        };

        var state = window.vmStates.IndexVM || {};
        state["selectedLink"] = "";
        return state;
    },
    render: function render() {
        var _this2 = this;

        var styles = {
            navMenu: { padding: "15px", color: "rgb(125,135,140)", backgroundColor: "rgb(40,50,55)" },
            link: { color: "#b8c7ce" },
            exampleLink: { color: "#b8c7ce", display: "block", padding: "5px 0px 5px 20px", textDecoration: "none" },
            activeExampleLink: { color: "black", backgroundColor: "#eef0f0", display: "block", padding: "5px 0px 5px 20px", textDecoration: "none" },
            desc: { padding: "0 15px 0 35px", fontSize: "9pt", marginBottom: ".5em" },
            header: { fontSize: "medium", color: "rgba(146,208,80,.8)" },
            list: { paddingLeft: "0", listStyleType: "none", margin: "0 -15px" },
            listItem: { paddingLeft: "20px" },
            bullet: { color: "rgba(255,205,0,.8)", transform: "scale(.5)" },
            copyright: { color: "rgb(125,135,140)", fontSize: "8pt" }
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

        return React.createElement(
            "div",
            null,
            React.createElement(
                "div",
                { classNames: "well", id: "pageHeader" },
                React.createElement(
                    "h1",
                    null,
                    React.createElement("span", { classNames: "circle" }),
                    React.createElement("img", { src: "/images/reactLogo.svg", width: "28" }),
                    "Tearc + ",
                    React.createElement(
                        "span",
                        null,
                        React.createElement("img", { src: "/images/reactLogo.svg", width: "28" }),
                        "Architect"
                    )
                ),
                React.createElement(
                    "p",
                    null,
                    this.state.LocalizedStrings != null ? this.state.LocalizedStrings.Slogan : ""
                ),
                React.createElement(
                    MuiThemeProvider,
                    null,
                    React.createElement(
                        "div",
                        { className: "row" },
                        React.createElement(
                            "div",
                            { className: "col-md-12" },
                            React.createElement(AppBar, { style: { marginBottom: "1em" },
                                iconElementRight: React.createElement(LanguageToggle, { onToggle: function (code) {
                                        return _this2.dispatch({ CultureCode: code });
                                    } }) })
                        )
                    )
                )
            ),
            React.createElement(
                "div",
                { classNames: "container-fluid" },
                React.createElement(
                    "div",
                    { className: "row" },
                    React.createElement(
                        "div",
                        { className: "col-md-2 nav-menu" },
                        React.createElement(
                            "div",
                            { id: "NavMenu" },
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
                                        showLinks(this.state.BasicExampleLinks)
                                    ),
                                    React.createElement(
                                        "h3",
                                        { style: styles.header },
                                        this.state.LocalizedStrings.ContactNav
                                    ),
                                    React.createElement(
                                        "ul",
                                        { id: "FurtherExamples", style: styles.list },
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
                                        "© 2017 Long Nguyen.  All code licensed under the ",
                                        React.createElement(
                                            "a",
                                            { style: styles.link, href: "http://www.apache.org/licenses/LICENSE-2.0" },
                                            "Apache license version 2.0"
                                        )
                                    ),
                                    React.createElement("br", null),
                                    React.createElement("br", null)
                                )
                            )
                        )
                    ),
                    React.createElement(
                        "div",
                        { className: "col-md-10" },
                        React.createElement("div", { id: "Content" })
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


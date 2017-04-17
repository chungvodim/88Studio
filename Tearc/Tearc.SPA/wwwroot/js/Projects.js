"use strict";

var RouteLink = dotnetify.react.router.RouteLink;

var Projects = React.createClass({
    displayName: "Projects",

    getInitialState: function getInitialState() {
        this.vm = dotnetify.react.connect("ProjectsVM", this);
        this.vm.onRouteEnter = function (path, template) {
            return template.Target = "ProjectPanel";
        };

        return window.vmStates.ProjectsVM;
    },
    componentWillUnmount: function componentWillUnmount() {
        this.vm.$destroy();
    },
    render: function render() {
        var _this = this;

        var styles = {
            root: {
                display: 'flex',
                flexWrap: 'wrap',
                justifyContent: 'space-around'
            },
            gridList: {
                width: '100%',
                height: 'auto',
                overflowY: 'none'
            }
        };

        var projectsData = [{
            ImageUrl: 'http://www.material-ui.com/images/grid-list/00-52-29-429_640.jpg',
            Title: 'Breakfast',
            Author: 'jill111'
        }, {
            ImageUrl: 'http://www.material-ui.com/images/grid-list/burger-827309_640.jpg',
            Title: 'Tasty burger',
            Author: 'pashminu'
        }, {
            ImageUrl: 'http://www.material-ui.com/images/grid-list/camera-813814_640.jpg',
            Title: 'Camera',
            Author: 'Danson67'
        }, {
            ImageUrl: 'http://www.material-ui.com/images/grid-list/morning-819362_640.jpg',
            Title: 'Morning',
            Author: 'fancycrave1'
        }, {
            ImageUrl: 'http://www.material-ui.com/images/grid-list/hats-829509_640.jpg',
            Title: 'Hats',
            Author: 'Hans'
        }, {
            ImageUrl: 'http://www.material-ui.com/images/grid-list/honey-823614_640.jpg',
            Title: 'Honey',
            Author: 'fancycravel'
        }, {
            ImageUrl: 'http://www.material-ui.com/images/grid-list/vegetables-790022_640.jpg',
            Title: 'Vegetables',
            Author: 'jill111'
        }, {
            ImageUrl: 'http://www.material-ui.com/images/grid-list/water-plant-821293_640.jpg',
            Title: 'Water plant',
            Author: 'BkrmadtyaKarki'
        }];

        if (this.state.Projects == null) {
            return React.createElement("div", null);
        } else {
            return React.createElement(
                MuiThemeProvider,
                null,
                React.createElement(
                    "div",
                    { style: styles.root },
                    React.createElement(
                        GridList,
                        {
                            cols: 4,
                            cellHeight: 'auto',
                            style: styles.gridList
                        },
                        React.createElement(
                            Subheader,
                            null,
                            "number of project: " + this.state.Projects.length
                        ),
                        this.state.Projects.map(function (project) {
                            return React.createElement(
                                GridTile,
                                {
                                    key: project.Info.ImageUrl,
                                    title: project.Info.Title,
                                    subtitle: React.createElement(
                                        "span",
                                        null,
                                        "by ",
                                        React.createElement(
                                            "b",
                                            null,
                                            project.Info.Author
                                        )
                                    ),
                                    actionIcon: React.createElement(
                                        IconButton,
                                        null,
                                        React.createElement(StarBorder, { color: "white" })
                                    )
                                },
                                React.createElement(
                                    RouteLink,
                                    { vm: _this.vm, route: project.Route },
                                    React.createElement("img", { src: project.Info.ImageUrl })
                                )
                            );
                        })
                    ),
                    React.createElement("div", { id: "ProjectPanel" })
                )
            );
        }
    }
});

var ProjectDefault = function ProjectDefault(props) {
    return React.createElement("div", null);
};

var Project = React.createClass({
    displayName: "Project",

    getInitialState: function getInitialState() {
        this.vm = dotnetify.react.connect("ProjectDetailsVM", this);
        return { Project: { Title: "", ImageUrl: "", Author: "", ItemUrl: "" }, open: true };
    },
    componentWillUnmount: function componentWillUnmount() {
        this.vm.$destroy();
    },
    render: function render() {
        var _this2 = this;

        var project = this.state.Project;

        var handleClose = function handleClose() {
            _this2.setState({ open: false });
            window.history.back();
        };

        var actions = [React.createElement(FlatButton, { label: "Back", primary: true, onTouchTap: handleClose })];

        return React.createElement(
            MuiThemeProvider,
            null,
            React.createElement(
                Dialog,
                { open: this.state.open, actions: actions },
                React.createElement(
                    "div",
                    { className: "row", style: { minHeight: "380px" } },
                    React.createElement(
                        "div",
                        { className: "col-md-4" },
                        React.createElement("img", { className: "thumbnail", src: project.ImageUrl })
                    ),
                    React.createElement(
                        "div",
                        { className: "col-md-8" },
                        React.createElement(
                            "h3",
                            null,
                            project.Title
                        ),
                        React.createElement(
                            "h5",
                            null,
                            project.Author
                        ),
                        React.createElement("br", null),
                        React.createElement(
                            RaisedButton,
                            { primary: true },
                            "Buy"
                        )
                    )
                )
            )
        );
    }
});


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
                        this.state.LocalizedStrings.ProjectNav
                    ),
                    projectsData.map(function (project) {
                        return React.createElement(
                            GridTile,
                            {
                                key: project.img,
                                title: project.title,
                                subtitle: React.createElement(
                                    "span",
                                    null,
                                    "by ",
                                    React.createElement(
                                        "b",
                                        null,
                                        project.author
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
                                { vm: _this.props.vm, route: project.Route },
                                React.createElement("img", { src: project.img })
                            )
                        );
                    })
                ),
                React.createElement("div", { id: "ProjectPanel" })
            )
        );
    }
});

var ProjectDefault = function ProjectDefault(props) {
    return React.createElement("div", null);
};

var Project = React.createClass({
    displayName: "Project",

    getInitialState: function getInitialState() {
        this.vm = dotnetify.react.connect("ProjectVM", this);
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


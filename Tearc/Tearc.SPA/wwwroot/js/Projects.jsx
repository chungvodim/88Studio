var RouteLink = dotnetify.react.router.RouteLink;

var Projects = React.createClass({

    getInitialState() {
        this.vm = dotnetify.react.connect("ProjectsVM", this);
        this.vm.onRouteEnter = (path, template) => template.Target = "ProjectPanel";

        var state = window.vmStates.ProjectsVM;
        console.log("number of project: " + state.Projects.length);
        return state;

    },
    componentWillUnmount() {
        this.vm.$destroy();
    },
    render() {
        const styles = {
            root: {
                display: 'flex',
                flexWrap: 'wrap',
                justifyContent: 'space-around',
            },
            gridList: {
                width: '100%',
                height: 'auto',
                overflowY: 'none',
            }
        }

        const projectsData = [
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/00-52-29-429_640.jpg',
                Title: 'Breakfast',
                Author: 'jill111',
            },
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/burger-827309_640.jpg',
                Title: 'Tasty burger',
                Author: 'pashminu',
            },
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/camera-813814_640.jpg',
                Title: 'Camera',
                Author: 'Danson67',
            },
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/morning-819362_640.jpg',
                Title: 'Morning',
                Author: 'fancycrave1',
            },
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/hats-829509_640.jpg',
                Title: 'Hats',
                Author: 'Hans',
            },
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/honey-823614_640.jpg',
                Title: 'Honey',
                Author: 'fancycravel',
            },
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/vegetables-790022_640.jpg',
                Title: 'Vegetables',
                Author: 'jill111',
            },
            {
                ImageUrl: 'http://www.material-ui.com/images/grid-list/water-plant-821293_640.jpg',
                Title: 'Water plant',
                Author: 'BkrmadtyaKarki',
            },
        ];

        return (
            <MuiThemeProvider>
                <div style={styles.root}>
                    <GridList
                        cols={4}
                        cellHeight={'auto'}
                        style={styles.gridList}
                    >
                        <Subheader>{"number of project: " + this.state.Projects.length}</Subheader>
                        {this.state.Projects.map((project) => (
                            <GridTile
                                key={project.Info.ImageUrl}
                                title={project.Info.Title}
                                subtitle={<span>by <b>{project.Info.Author}</b></span>}
                                actionIcon={<IconButton><StarBorder color="white" /></IconButton>}
                            >
                                <RouteLink vm={this.vm} route={project.Route}>
                                    <img src={project.Info.ImageUrl} />
                                </RouteLink>
                            </GridTile>
                        ))}
                    </GridList>
                    <div id="ProjectPanel" />
                </div>
            </MuiThemeProvider>
        );
    }
});

var ProjectDefault = function (props) {
    return <div></div>;
}

var Project = React.createClass({
    getInitialState() {
        this.vm = dotnetify.react.connect("ProjectVM", this);
        return { Project: { Title: "", ImageUrl: "", Author: "", ItemUrl: "" }, open: true };
    },
    componentWillUnmount() {
        this.vm.$destroy();
    },
    render() {
        var project = this.state.Project;

        const handleClose = () => {
            this.setState({ open: false });
            window.history.back();
        }

        const actions = [<FlatButton label="Back" primary={true} onTouchTap={handleClose} />]

        return (
            <MuiThemeProvider>
                <Dialog open={this.state.open} actions={actions}>
                    <div className="row" style={{ minHeight: "380px" }}>
                        <div className="col-md-4">
                            <img className="thumbnail" src={project.ImageUrl} />
                        </div>
                        <div className="col-md-8">
                            <h3>{project.Title}</h3>
                            <h5>{project.Author}</h5>
                            <br />
                            <RaisedButton primary={true}>Buy</RaisedButton>
                        </div>
                    </div>
                </Dialog>
            </MuiThemeProvider>
        )
    }
});
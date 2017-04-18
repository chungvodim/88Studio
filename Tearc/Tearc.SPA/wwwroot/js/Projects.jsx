var RouteLink = dotnetify.react.router.RouteLink;

var Projects = React.createClass({

    getInitialState() {
        this.vm = dotnetify.react.connect("ProjectsVM", this);
        this.vm.onRouteEnter = (path, template) => template.Target = "ProjectPanel";

        return window.vmStates.ProjectsVM;

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

        if (this.state.Projects == null) {
            return <div></div>
        }
        else {
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
    }
});

var ProjectDefault = function (props) {
    return <div></div>;
}

var Project = React.createClass({
    getInitialState() {
        this.vm = dotnetify.react.connect("ProjectDetailsVM", this);
        return { Project: { Title: "", ImageUrl: "", ImageUrls: [], Author: "", ItemUrl: "" }, open: true };
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
                overflowY: 'auto',
                flexWrap: 'nowrap',
                overflowX: 'auto',
            }
        }

        var project = this.state.Project;

        const handleClose = () => {
            this.setState({ open: false });
            window.history.back();
        }

        const actions = [<FlatButton label="Back" primary={true} onTouchTap={handleClose} />]

        console.log("fuck" + project.ImageUrls.length);

        const gridTiles = project.ImageUrls.map(imageUrl =>
            <GridTile
            >
                <img src={imageUrl} />
            </GridTile>
        )

        return (
            <MuiThemeProvider>
                <Dialog open={this.state.open} actions={actions}>
                    <div className="row" style={{ minHeight: "380px" }}>
                        <div className="col-md-12">
                            <GridList
                                cellHeight={200}
                                style={styles.gridList}
                            >
                                {gridTiles}
                            </GridList>
                        </div>
                        <div className="col-md-12">
                            <h3>{project.Title}</h3>
                            <h5>{project.Author}</h5>
                        </div>
                    </div>
                </Dialog>
            </MuiThemeProvider>
        )
    }
});
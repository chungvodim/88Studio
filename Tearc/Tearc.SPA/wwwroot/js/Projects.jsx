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

        const projectsData = [
            {
                img: 'http://www.material-ui.com/images/grid-list/00-52-29-429_640.jpg',
                title: 'Breakfast',
                author: 'jill111',
            },
            {
                img: 'http://www.material-ui.com/images/grid-list/burger-827309_640.jpg',
                title: 'Tasty burger',
                author: 'pashminu',
            },
            {
                img: 'http://www.material-ui.com/images/grid-list/camera-813814_640.jpg',
                title: 'Camera',
                author: 'Danson67',
            },
            {
                img: 'http://www.material-ui.com/images/grid-list/morning-819362_640.jpg',
                title: 'Morning',
                author: 'fancycrave1',
            },
            {
                img: 'http://www.material-ui.com/images/grid-list/hats-829509_640.jpg',
                title: 'Hats',
                author: 'Hans',
            },
            {
                img: 'http://www.material-ui.com/images/grid-list/honey-823614_640.jpg',
                title: 'Honey',
                author: 'fancycravel',
            },
            {
                img: 'http://www.material-ui.com/images/grid-list/vegetables-790022_640.jpg',
                title: 'Vegetables',
                author: 'jill111',
            },
            {
                img: 'http://www.material-ui.com/images/grid-list/water-plant-821293_640.jpg',
                title: 'Water plant',
                author: 'BkrmadtyaKarki',
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
                        <Subheader>{this.state.LocalizedStrings.ProjectNav}</Subheader>
                        {projectsData.map((project) => (
                            <GridTile
                                key={project.img}
                                title={project.title}
                                subtitle={<span>by <b>{project.author}</b></span>}
                                actionIcon={<IconButton><StarBorder color="white" /></IconButton>}
                            >
                                <RouteLink vm={this.props.vm} route={project.Route}>
                                    <img src={project.img} />
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
'use strict';

var Projects = React.createClass({
    displayName: 'Projects',

    getInitialState: function getInitialState() {
        var _this = this;

        // Connect this component to the back-end view model.
        this.vm = dotnetify.react.connect("ProjectsVM", this);

        // Set up function to dispatch state to the back-end.
        this.dispatchState = function (state) {
            _this.setState(state);
            // $dispatch: Dispatches a value to the server view model. this is built-in function of Dotnetify
            _this.vm.$dispatch(state);
            // setState: This is the primary method you use to trigger UI updates from event handlers and server request callbacks
        };

        // This component's JSX was loaded along with the VM's initial state for faster rendering.
        return window.vmStates.ProjectsVM;
    },
    componentWillUnmount: function componentWillUnmount() {
        this.vm.$destroy();
    },
    render: function render() {
        var styles = {
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

        return React.createElement(
            MuiThemeProvider,
            null,
            React.createElement(
                'div',
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
                        'December'
                    ),
                    tilesData.map(function (tile) {
                        return React.createElement(
                            GridTile,
                            {
                                key: tile.img,
                                title: tile.title,
                                subtitle: React.createElement(
                                    'span',
                                    null,
                                    'by ',
                                    React.createElement(
                                        'b',
                                        null,
                                        tile.author
                                    )
                                ),
                                actionIcon: React.createElement(
                                    IconButton,
                                    null,
                                    React.createElement(StarBorder, { color: 'white' })
                                )
                            },
                            React.createElement('img', { src: tile.img })
                        );
                    })
                )
            )
        );
    }
});


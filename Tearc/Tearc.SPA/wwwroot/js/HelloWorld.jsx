﻿var HelloWorld = React.createClass({

    getInitialState() {
        // Connect this component to the back-end view model.
        this.vm = dotnetify.react.connect("HelloWorldVM", this);

        // Set up function to dispatch state to the back-end.
        this.dispatchState = state => {
            this.setState(state);
            // $dispatch: Dispatches a value to the server view model. this is built-in function of Dotnetify
            this.vm.$dispatch(state);
            // setState: This is the primary method you use to trigger UI updates from event handlers and server request callbacks
        }

        // This component's JSX was loaded along with the VM's initial state for faster rendering.
        return window.vmStates.HelloWorldVM;
    },
    componentWillUnmount() {
        this.vm.$destroy();
    },
    render() {
        return (
            <div className="container-fluid">
                <div className="header clearfix">
                    <h3>Example: Hello World</h3>
                </div>
                <div className="jumbotron">
                    <div className="row">
                        <div className="col-md-6">
                            <TextBox data={{ Label: "First name:", Placeholder: "Type first name here" }}
                                value={this.state.FirstName}
                                onChange={value => this.setState({ FirstName: value })}
                                onUpdate={value => this.dispatchState({ FirstName: value })} />
                        </div>
                        <div className="col-md-6">
                            <TextBox data={{ Label: "Last name:", Placeholder: "Type last name here" }}
                                value={this.state.LastName}
                                onChange={value => this.setState({ LastName: value })}
                                onUpdate={value => this.dispatchState({ LastName: value })} />
                        </div>
                    </div>
                    <hr />
                    <div>
                        Full name is <b><span>{this.state.FullName}</span></b>
                    </div>
                </div>
            </div>
        );
    }
});

var TextBox = React.createClass({
    getInitialState() {
        return {
            changed: false
        }
    },
    handleChange(event) {
        this.setState({ changed: true });
        this.props.onChange(event.target.value);
    },
    handleBlur() {
        if (this.state.changed)
            this.props.onUpdate(this.props.value);
        this.setState({ changed: false });
    },
    render() {
        return (
            <div>
                <label>{this.props.data.Label}</label>
                <input className="form-control" type="text"
                    value={this.props.value}
                    placeholder={this.props.data.Placeholder}
                    onChange={this.handleChange}
                    onBlur={this.handleBlur} />
            </div>
        );
    }
});
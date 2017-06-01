import React from "react";
import Menu from "./components/common/Menu";
import "./App.css";
import logo from './logo.svg';

export default class App extends React.Component {
    // render
    render() {
        const {children} = this.props;

        return (
            <div className="App">
                {/*<div>*/}
                {/*<Menu/>*/}
                {/*</div>*/}
                <div className="App-header">
                    <img src={logo} className="App-logo" alt="logo" />
                    <h2>Welcome to React</h2>
                </div>
                <div className="App-intro">
                    {children}
                </div>
                <div className="footer">
                    <span>Tearc - 88Studio</span>
                </div>
            </div>
        );
    }
}

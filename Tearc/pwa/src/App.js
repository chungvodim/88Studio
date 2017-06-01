import React from "react";
import Menu from "./components/common/Menu";
import "./App.css";
import logo from './logo.svg';

export default class App extends React.Component {
    // render
    render() {
        const {children} = this.props;
        const divStyle = {
            margin: '0 15px',
        };
        return (
            <div className="App">
                <div>
                    <Menu/>
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

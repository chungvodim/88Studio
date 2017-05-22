import React from "react";
import { connect } from "react-redux";
import { ProgressBar } from "react-bootstrap";
import Menu from "./common/Menu";
import "../stylesheets/main.scss";

// App component
// This is root component
export default class App extends React.Component {
  // render
  render() {
    // show the loading state while we wait for the app to load
    const {children} = this.props;

    // render
    return (
      <div className="container">
        <div>
          <Menu/>
        </div>
        <div>
          {/*child component*/}
          {children}
        </div>
        <div className="footer">
          {/*<img src="/media/logo.svg"/>*/}
          <span>
            Tearc - 88Studio
          </span>
        </div>
      </div>
    );
  }
}

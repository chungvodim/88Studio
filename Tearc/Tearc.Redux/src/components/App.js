import React from "react";
import { connect } from "react-redux";
import { ProgressBar } from "react-bootstrap";
import Menu from "./common/Menu";
import "../stylesheets/main.scss";

// App component
// This is root component
export class App extends React.Component {
  // pre-render logic
  componentWillMount() {
    // the first time we load the app, we need that users list
    // this.props.dispatch({type: 'USERS_FETCH_LIST'});
    this.props.dispatch({type: 'GALLERY_FETCH_LIST'});
  }

  // render
  render() {
    // show the loading state while we wait for the app to load
    const {galleries, children} = this.props;
    if (!galleries.length) {
      return (
        <ProgressBar active now={100}/>
      );
    }

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

// export the connected class
function mapStateToProps(state) {
  return {
    galleries: state.galleries || [],
  };
}
export default connect(mapStateToProps)(App);

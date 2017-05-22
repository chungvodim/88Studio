import React from "react";
import { connect } from "react-redux";
import { ProgressBar } from "react-bootstrap";
import Galleries from "../components/galerry/Galleries";
import UserList from "../components/user/UserList";

// Home page component
export class Home extends React.Component {
  // pre-render logic
  componentWillMount() {
    // the first time we load the app, we need that users list
    // this.props.dispatch({type: 'USERS_FETCH_LIST'});
    this.props.dispatch({type: 'GALLERY_FETCH_LIST'});
  }
  // render
  render() {
    const {galleries} = this.props;
    if (!galleries.length) {
      return (
        <ProgressBar active now={100}/>
      );
    }
    return (
      <div className="page-home">
        <Galleries/>
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
export default connect(mapStateToProps)(Home);

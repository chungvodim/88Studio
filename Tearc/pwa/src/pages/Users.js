import React from "react";
import { connect } from "react-redux";
import { ProgressBar } from "react-bootstrap";
import UserList from "../components/user/UserList";

// Home page component
export class Users extends React.Component {
  // pre-render logic
  componentWillMount() {
    // the first time we load the app, we need that users list
    // this.props.dispatch({type: 'USERS_FETCH_LIST'});
    this.props.dispatch({type: 'USERS_FETCH_LIST'});
  }
  // render
  render() {
    const {users} = this.props;
    if (!users.length) {
      return (
        <ProgressBar active now={100}/>
      );
    }
    return (
      <div className="page-users">
        <UserList/>
      </div>
    );
  }
}

// export the connected class
function mapStateToProps(state) {
  return {
    users: state.users || [],
  };
}
export default connect(mapStateToProps)(Users);

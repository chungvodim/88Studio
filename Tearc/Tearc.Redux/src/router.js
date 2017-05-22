import React from "react";
import { Router, Route, IndexRoute } from "react-router";
import { history } from "./store.js";
import App from "./components/App";
import Home from "./pages/Home";
import Users from "./pages/Users";
import UserEdit from "./pages/UserEdit";
import NotFound from "./pages/NotFound";


// build the router
const router = (
  <Router onUpdate={() => window.scrollTo(0, 0)} history={history}>
    <Route path="/" component={App}>
      <IndexRoute component={Home}/>
      {/*Add new page here*/}
      <Route path="users" component={Users}/>
      <Route path="user-edit(/:id)" component={UserEdit}/>
      <Route path="*" component={NotFound}/>
    </Route>
  </Router>
);

// export
export { router };

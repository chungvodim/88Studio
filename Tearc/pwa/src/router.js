import React from "react";
import { Router, Route, IndexRoute } from "react-router";
import { history } from "./store.js";
import App from "./App";
import Home from "./pages/Home";
import Contact from "./pages/Contact";
import Users from "./pages/Users";
import UserEdit from "./pages/UserEdit";
import NotFound from "./pages/NotFound";


// build the router
const router = (
  <Router onUpdate={() => window.scrollTo(0, 0)} history={history}>
    <Route path="/" component={App}>
      <IndexRoute component={Home}/>
      {/*Add new page here*/}
      <Route path="contact" component={Contact}/>
      <Route path="users" component={Users}/>
      <Route path="user-edit(/:id)" component={UserEdit}/>
      <Route path="*" component={NotFound}/>
    </Route>
  </Router>
);

// export
export { router };

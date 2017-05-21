import { takeLatest } from "redux-saga";
import { fork } from "redux-saga/effects";
import { usersFetchList, usersAddEdit, usersDelete } from "./users";

// main saga generators
// fork: Creates an Effect description that instructs the middleware to perform a non-blocking call on fn
// takeLatest: Spawns a saga on each action dispatched to the Store that matches pattern. And automatically cancels any previous saga task started previous if it's still running.
export function* sagas() {
  yield [
    fork(takeLatest, 'USERS_FETCH_LIST', usersFetchList),
    fork(takeLatest, 'USERS_ADD_EDIT', usersAddEdit),
    fork(takeLatest, 'USERS_DELETE', usersDelete),
  ];
}

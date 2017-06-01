import { takeLatest } from "redux-saga";
import { fork } from "redux-saga/effects";
import { usersFetchList, usersAddEdit, usersDelete } from "./users";
import { galleryFetchList, galleryAddEdit, galleryDelete } from "./galleries";

// main saga generators
// fork: Creates an Effect description that instructs the middleware to perform a non-blocking call on fn
// takeLatest: Spawns a saga on each action dispatched to the Store that matches pattern. And automatically cancels any previous saga task started previous if it's still running.
export function* sagas() {
  yield [
    fork(takeLatest, 'USERS_FETCH_LIST', usersFetchList),
    fork(takeLatest, 'USERS_ADD_EDIT', usersAddEdit),
    fork(takeLatest, 'USERS_DELETE', usersDelete),

    fork(takeLatest, 'GALLERY_FETCH_LIST', galleryFetchList),
    fork(takeLatest, 'GALLERY_ADD_EDIT', galleryAddEdit),
    fork(takeLatest, 'GALLERY_DELETE', galleryDelete),
  ];
}

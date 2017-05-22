import { call, put } from "redux-saga/effects";
import ApiGallery from "../api/gallery";

// fetch the gallery
export function* galleryFetchList(action) {
  // call the api to get the gallery
  // call: Creates an Effect description that instructs the middleware to call the function fn with args as arguments.
  const galleries = yield call(ApiGallery.getList);

  // save the gallery in state
  yield put({
    type: 'GALLARY_FETCH_LIST_SAVE',
    galleries: galleries,
  });
}

// add/edit a gallery
export function* galleryAddEdit(action) {
  // call the api to add/edit gallery
  yield call(ApiGallery.addEdit);
  //return action.callbackError("Some error");   // show an error when the API fails

  // update the state by adding/editing the gallery
  yield put({
    type: action.gallery.id ? 'GALLERY_EDIT_SAVE' : 'GALLERY_ADD_SAVE',
    gallery: action.gallery,
  });

  // success
  action.callbackSuccess();
}

// delete a gallery
export function* galleryDelete(action) {
  // call the api to delete the gallery
  yield call(ApiGallery.delete);

  // update the state by removing the gallery
  yield put({
    type: 'GALLERY_DELETE_SAVE',
    gallery_id: action.gallery_id,
  });
}

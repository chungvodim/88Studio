// gallery reducer
export default function gallery(state = {}, action) {
  switch (action.type) {
    case 'GALLERY_FETCH_LIST_SAVE':
      return action.galleries;

    case 'GALLERY_ADD_SAVE':
      const gallery = action.gallery;
      gallery.id = gallery.id || Math.floor(Math.random() * (9999 - 1000 + 1)) + 1000;
      return [...state, gallery];

    case 'GALLERY_EDIT_SAVE':
      return state.map(gallery =>
        Number(gallery.id) === Number(action.gallery.id) ? {...action.gallery} : gallery
      );
      break;

    case 'GALLERY_DELETE_SAVE':
      return state.filter(user =>
        Number(user.id) !== Number(action.gallery_id)
      );

    // initial state
    default:
      return state;
  }
}

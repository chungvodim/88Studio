import React from "react";
import { connect } from "react-redux";
import { push } from "react-router-redux";
import { Table, Pagination } from "react-bootstrap";
import GalleryListElement from "./GalleryListElement";
import GalleryDeletePrompt from "./GalleryDeletePrompt";

// Gallery list component
export class Galleries extends React.Component {
  // constructor
  constructor(props) {
    super(props);

    // default ui local state
    this.state = {
      delete_show: false,
      delete_gallery: {},
    };

    // bind <this> to the event method
    this.changePage = this.changePage.bind(this);
    this.showDelete = this.showDelete.bind(this);
    this.hideDelete = this.hideDelete.bind(this);
    this.galleryDelete = this.galleryDelete.bind(this);
  }

  // render
  render() {
    // pagination
    // Get props from state : mapStateToProps
    // In Redux, there're only 1 state that is managed by store
    const {galleries, page} = this.props;
    const per_page = 10;
    const pages = Math.ceil(galleries.length / per_page);
    const start_offset = (page - 1) * per_page;
    let start_count = 0;

    // show the list of galleries
    return (
      <div>
        <Table bordered hover responsive striped>
          <thead>
          <tr>
            <th>ID</th>
            <th>Title</th>
            <th>URL</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
          </thead>
          <tbody>
          {galleries.map((gallery, index) => {
            if (index >= start_offset && start_count < per_page) {
              start_count++;
              return (
                <GalleryListElement key={index} gallery={gallery} showDelete={this.showDelete}/>
              );
            }
          })}
          </tbody>
        </Table>

        <Pagination className="galleries-pagination pull-right" bsSize="medium" maxButtons={10} first last next
          prev boundaryLinks items={pages} activePage={page} onSelect={this.changePage}/>

        <GalleryDeletePrompt show={this.state.delete_show} gallery={this.state.delete_gallery}
          hideDelete={this.hideDelete} galleryDelete={this.galleryDelete}/>
      </div>
    );
  }

  // change the gallery lists' current page
  changePage(page) {
    this.props.dispatch(push('/?page=' + page));
  }

  // show the delete gallery prompt
  showDelete(gallery) {
    // change the local ui state
    this.setState({
      delete_show: true,
      delete_gallery: gallery,
    });
  }

  // hide the delete gallery prompt
  hideDelete() {
    // change the local ui state
    this.setState({
      delete_show: false,
      delete_gallery: {},
    });
  }

  // delete the gallery
  galleryDelete() {
    // delete the gallery
    this.props.dispatch({
      type: 'GALLERY_DELETE',
      gallery_id: this.state.delete_gallery.id,
    });

    // hide the prompt
    this.hideDelete();
  }
}

// export the connected class
function mapStateToProps(state) {
  return {
    galleries: state.galleries,

    // https://github.com/reactjs/react-router-redux#how-do-i-access-router-state-in-a-container-component
    // react-router-redux wants you to get the url data by passing the props through a million components instead of
    // reading it directly from the state, which is basically why you store the url data in the state (to have access to it)
    page: Number(state.routing.locationBeforeTransitions.query.page) || 1,
  };
}
export default connect(mapStateToProps)(Galleries);

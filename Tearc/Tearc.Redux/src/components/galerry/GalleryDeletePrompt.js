import React, { PropTypes } from "react";
import { Modal, Button } from "react-bootstrap";

// Gallery delete component
export default class GalleryDeletePrompt extends React.Component {
  // render
  render() {
    const {show, gallery, hideDelete, galleryDelete} = this.props;
    return (
      <Modal show={show}>
        <Modal.Header>
          <Modal.Title>
            Are you sure you want to delete <strong>{gallery.title}</strong>?
          </Modal.Title>
        </Modal.Header>
        <Modal.Footer>
          <Button onClick={hideDelete}>No</Button>
          <Button bsStyle="primary" onClick={galleryDelete}>Yes</Button>
        </Modal.Footer>
      </Modal>
    );
  }
}

// prop checks
GalleryDeletePrompt.propTypes = {
  show: PropTypes.bool.isRequired,
  gallery: PropTypes.object.isRequired,
  hideDelete: PropTypes.func.isRequired,
  galleryDelete: PropTypes.func.isRequired,
}

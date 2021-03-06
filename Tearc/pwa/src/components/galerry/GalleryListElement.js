import React from "react";
import PropTypes from 'prop-types';
import { Link } from "react-router";
import { Button, Glyphicon } from "react-bootstrap";

// Gallery List Element component
export default class GalleryListElement extends React.Component {
  // render
  render() {
    // Get props directly from DOM
    const {gallery, showDelete} = this.props;
    return (
      <tr>
        <td>{gallery.title}</td>
        <td><img alt="Tearc-88Studio" src={gallery.url}/></td>
        <td>
          <Link to={'gallery-edit/' + gallery.id}>
            <Button bsSize="xsmall">
              Edit <Glyphicon glyph="edit"/>
            </Button>
          </Link>
        </td>
        <td>
          <Button bsSize="xsmall" className="gallery-delete" onClick={() => showDelete(gallery)}>
            Delete <Glyphicon glyph="remove-circle"/>
          </Button>
        </td>
      </tr>
    );
  }
}

// prop checks
GalleryListElement.propTypes = {
  gallery: PropTypes.object.isRequired,
  showDelete: PropTypes.func.isRequired,
}

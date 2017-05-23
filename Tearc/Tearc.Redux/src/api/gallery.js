const PREFIX_URL = 'https://raw.githubusercontent.com/xiaolin/react-image-gallery/master/static/';
// API Gallery static class
export default class ApiGallery {
  // get galleries
  static getList() {
    return new Promise(resolve => {
      setTimeout(() => {
        // build some dummy galleries list
        let galleries = ApiGallery._getVideos().concat(ApiGallery._getStaticImages());
        resolve(galleries);
      }, 200);
    });
  }

  // add/edit a gallery
  static addEdit() {
    return new Promise(resolve => {
      // do something here
      resolve();
    });
  }

  // delete a gallery
  static delete() {
    return new Promise(resolve => {
      // do something here
      resolve();
    });
  }

  // fake data
  static _getStaticImages() {
    let galleries = [];
    for (let i = 2; i < 12; i++) {
      galleries.push({
        original: `${PREFIX_URL}${i}.jpg`,
        thumbnail:`${PREFIX_URL}${i}t.jpg`
      });
    }

    return galleries;
  }

  static _getVideos() {
    let galleries = [
      {
        original: `${PREFIX_URL}1.jpg`,
        thumbnail: `${PREFIX_URL}1t.jpg`,
        originalClass: 'featured-slide',
        thumbnailClass: 'featured-thumb',
        description: '88Studio'
      },
      {
        thumbnail: `${PREFIX_URL}3v.jpg`,
        original: `${PREFIX_URL}3v.jpg`,
        embedUrl: 'https://www.youtube.com/embed/iNJdPyoqt8U?autoplay=1&showinfo=0',
        description: '88Studio'
      },
      {
        thumbnail: `${PREFIX_URL}4v.jpg`,
        original: `${PREFIX_URL}4v.jpg`,
        embedUrl: 'https://www.youtube.com/embed/4pSzhZ76GdM?autoplay=1&showinfo=0',
        description: '88Studio'
      }
    ];

    return galleries;
  }
}

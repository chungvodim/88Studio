// API Gallery static class
export default class ApiGallery {
  // get galleries
  static getList() {
    return new Promise(resolve => {
      // build some dummy galleries list
      let galleries = [];
      for (let x = 1; x <= 28; x++) {
        galleries.push({
          id: x,
          title: 'image ' + x,
          url: 'http://2.pik.vn/2017c79ad7bf-91e7-4d04-9f99-dc00c9b3f0d8.jpg',
        });
      }
      console.log("galleries length: " + galleries.length);
      resolve(galleries);
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
}

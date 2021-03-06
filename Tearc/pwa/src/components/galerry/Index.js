import React from 'react';
import Gallery from 'react-photo-gallery';
import $ from 'jquery';
import _ from 'lodash';
import Measure from 'react-measure';
import Lightbox from 'react-images';
import './Index.css';

export default class Index extends React.Component {
    constructor(){
        super();
        this.state = {
            photos:null,
            pageNum:1,
            totalPages:1,
            loadedAll: false,
            currentImage:0,
            url: 'https://api.flickr.com/services/rest/',
            get_photos_method: 'flickr.photosets.getPhotos',
            get_list_method: 'flickr.photosets.getList',
            get_popular_method: 'flickr.photos.getPopular',
            api_key: '3bceb192ae7e6efee09f0b097b0ba3bc',
            user_id: '123607232@N08',
            photoset_id: '',
            per_page: '12',
            photoSet_IDs: [],
            photoSetIndex: 0,
            numberOfPhotoSet: 0,
        };
        this.handleScroll = this.handleScroll.bind(this);
        this.loadMorePhotos = this.loadMorePhotos.bind(this);
        this.closeLightbox = this.closeLightbox.bind(this);
        this.openLightbox = this.openLightbox.bind(this);
        this.gotoNext = this.gotoNext.bind(this);
        this.gotoPrevious = this.gotoPrevious.bind(this);
        this.getList = this.getList.bind(this);
        this.isSamePhotoSet = this.isSamePhotoSet.bind(this);
    }
    componentWillMount() {
    }
    componentDidMount() {
        this.getList();
        this.loadMorePhotos = _.debounce(this.loadMorePhotos, 200);
        window.addEventListener('scroll', this.handleScroll);
    }
    handleScroll(){
        let scrollY = window.scrollY || window.pageYOffset || document.documentElement.scrollTop;
        if ((window.innerHeight + scrollY) >= (document.body.offsetHeight - 50)) {
            this.loadMorePhotos();
        }
    }
    getList(){
        $.ajax({
            url: this.state.url+ '?' +
            'method='+this.state.get_list_method +
            '&api_key='+this.state.api_key +
            '&user_id='+this.state.user_id +
            '&nojsoncallback=1'+
            '&format=json',
            dataType: 'json',
            cache: false,
            success: function(data) {
                let photoset_IDs = [];
                data.photosets.photoset.forEach(function(obj,i,array){
                    photoset_IDs.push(obj.id);
                })
                this.setState({
                    photoSet_IDs: photoset_IDs,
                    photoSetIndex: 0,
                    numberOfPhotoSet: photoset_IDs.length,
                    photoset_id: photoset_IDs[this.state.photoSetIndex]
                });
                // console.log(this.state.photoSet_IDs);
                this.loadMorePhotos();
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(status, err.toString());
            }
        });
    }
    loadMorePhotos(e){
        if (e){
            e.preventDefault();
        }
        if (this.state.pageNum > this.state.totalPages){
            if(this.state.photoSetIndex + 1 < this.state.numberOfPhotoSet){
                // go through next photo set
                this.setState({
                    photoSetIndex: this.state.photoSetIndex + 1,
                    photoset_id: this.state.photoSet_IDs[this.state.photoSetIndex + 1],
                    pageNum: 1,
                });
            } else {
                console.log('no more photos');
                this.setState({loadedAll: true});
                return;
            }
        }
        let url = this.state.url +'?'+
            'method='+this.state.get_photos_method+
            '&api_key='+this.state.api_key+
            '&photoset_id='+this.state.photoset_id+
            '&user_id='+this.state.user_id+
            '&format=json'+
            '&per_page='+this.state.per_page+
            '&page='+this.state.pageNum+
            '&nojsoncallback=1'+
            '&extras=url_m,url_c,url_l,url_h,url_o';
        // console.log('url: ' + url);
        let photoset_id = this.state.photoset_id;
        $.ajax({
            url: url,
            dataType: 'json',
            cache: false,
            success: function(data) {
                let photos = [];
                // console.log(data);
                if(data.stat === 'ok'){
                    data.photoset.photo.forEach(function(obj,i,array){
                        let aspectRatio = parseFloat(obj.width_o / obj.height_o);
                        photos.push({
                            src: (aspectRatio >= 3) ? obj.url_c : obj.url_m,
                            width: parseInt(obj.width_o),
                            height: parseInt(obj.height_o),
                            caption: obj.title,
                            alt: obj.title,
                            srcset:[
                                obj.url_m+' '+obj.width_m+'w',
                                obj.url_c+' '+obj.width_c+'w',
                                obj.url_l+' '+obj.width_l+'w',
                                obj.url_h+' '+obj.width_h+'w'
                            ],
                            sizes:[
                                '(min-width: 480px) 50vw',
                                '(min-width: 1024px) 33.3vw',
                                '100vw'
                            ],
                            photoset_id: photoset_id
                        });
                    })
                    this.setState({
                        photos: this.state.photos ? this.state.photos.concat(photos) : photos,
                        pageNum: this.state.pageNum + 1,
                        totalPages: data.photoset.pages
                    });
                } else {
                    console.log(data.message);
                }

            }.bind(this),
            error: function(xhr, status, err) {
                console.error(status, err.toString());
            }
        });
    }
    openLightbox(index, event){
        event.preventDefault();
        console.log("this.state.photos[index].photoset_id: "+ this.state.photos[index].photoset_id) ;
        this.setState({
            currentImage: index,
            lightboxIsOpen: true
        });
    }
    closeLightbox(){
        this.setState({
            currentImage: 0,
            lightboxIsOpen: false,
        });
    }
    gotoPrevious(){
        let curImage = this.state.currentImage;
        let prevImage = this.state.currentImage - 1;
        if(!this.isSamePhotoSet(curImage, prevImage)){
            return;
        }
        this.setState({
            currentImage: this.state.currentImage - 1,
        });
    }
    gotoNext(){
        let curImage = this.state.currentImage;
        let nextImage = this.state.currentImage + 1;
        if(!this.isSamePhotoSet(curImage, nextImage)){
            return;
        }
        this.setState({
            currentImage: this.state.currentImage + 1,
        });
    }
    isSamePhotoSet(index1, index2){
        if(this.state.photos[index1].photoset_id != this.state.photos[index2].photoset_id){
            return false;
        }else{
            return true
        }
    }

    renderGallery(){
        return(
            <Measure whitelist={['width']}>
                {
                    ({ width }) => {
                        var cols = 1;
                        if (width >= 480){
                            cols = 2;
                        }
                        if (width >= 1024){
                            cols = 3;
                        }
                        return <Gallery photos={this.state.photos} cols={cols} onClickPhoto={this.openLightbox} />
                    }
                }
            </Measure>
        );
    }
    render(){
        // no loading sign if its all loaded
        if (this.state.photos){
            return(
                <div>
                    {this.renderGallery()}
                    <Lightbox
                        images={this.state.photos}
                        backdropClosesModal={true}
                        onClose={this.closeLightbox}
                        onClickPrev={this.gotoPrevious}
                        onClickNext={this.gotoNext}
                        currentImage={this.state.currentImage}
                        isOpen={this.state.lightboxIsOpen}
                        width={1600}
                    />
                    {!this.state.loadedAll && <div className="loading-msg" id="msg-loading-more">Loading</div>}
                </div>
            );
        }
        else{
            return(
                <div className="App">
                    <div id="msg-app-loading" className="loading-msg">Loading</div>
                </div>
            );
        }
    }
}

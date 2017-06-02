import React from "react";

export default class Contact extends React.Component {
    render() {
        return (
            <div className="page-contact">
                <iframe className="contact-map" src={"https://www.google.com/maps/embed/v1/place?key=AIzaSyB1ZgjCbu06PnojmNhGGQ7EKjFZGmJLdvU&q=place_id:ChIJU6eTZTMYQjERSiYeS0eImys"} allowfullscreen>
                </iframe>
                <div className="page-contact-address">
                    <p>
                        <b>88Studio</b>
                    </p>
                    <p>
                        164 Nguyễn Chí Thanh - Hải Châu - Đà Nẵng - Việt Nam
                    </p>
                    <p>
                        Tel: 02362220488
                    </p>
                </div>
            </div>
        );
    }
}
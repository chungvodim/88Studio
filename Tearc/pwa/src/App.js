import React from "react";
import Particles from "react-particles-js";
import Menu from "./components/common/Menu";
import "./App.css";
import logo from './logo.svg';

export default class App extends React.Component {
    // render
    render() {
        const {children} = this.props;

        return (
            <div className="App">
                <Particles
                    params={{
                        particles: {
                            number: {
                                value: 100
                            },
                            line_linked: {
                                shadow: {
                                    enable: false,
                                    color: "#3CA9D1",
                                    blur: 5
                                }
                            },
                        },
                        interactivity: {
                            detect_on: "canvas",
                            events: {
                                onhover: {
                                    enable: true,
                                    mode: "grab"
                                },
                                onclick: {
                                    enable: true,
                                    mode: "push"
                                },
                                resize: true
                            },
                            modes: {
                                grab: {
                                    distance: 140,
                                    line_linked: {
                                        opacity: 1
                                    }
                                },
                                bubble: {
                                    distance: 400,
                                    size: 40,
                                    duration: 2,
                                    opacity: 8,
                                    speed: 3
                                },
                                repulse: {
                                    distance: 200,
                                    duration: 0.4
                                },
                                push: {
                                    particles_nb: 4
                                },
                                remove: {
                                    particles_nb: 2
                                }
                            }
                        },
                        retina_detect: true
                    }}
                    style={{
                        position: "fixed",
                        top: 0,
                        left: 0,
                        width: "100%",
                        height: "100%"
                    }} />
                <div className="App-header">
                    <Menu/>
                </div>
                <div className="App-intro">
                    {children}
                </div>
                <div className="footer">
                    <span>Tearc - 88Studio</span>
                </div>
            </div>
        );
    }
}

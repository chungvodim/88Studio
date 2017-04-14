var RouteLink = dotnetify.react.router.RouteLink;

var Index = React.createClass({

   getInitialState() {
      // Connect this component to the back-end view model.
      this.vm = dotnetify.react.connect("IndexVM", this);
      this.vm.onRouteEnter = (path, template) => template.Target = "Content";

      var state = window.vmStates.IndexVM || {};
      state["selectedLink"] = "";
      return state;
   },
   render() {
      const styles = {
         navMenu: { padding: "15px", color: "rgb(125,135,140)", backgroundColor: "rgb(40,50,55)" },
         link: { color: "#b8c7ce" },
         exampleLink: { color: "#b8c7ce", display: "block", padding: "5px 0px 5px 20px", textDecoration: "none" },
         activeExampleLink: { color: "black", backgroundColor: "#eef0f0", display: "block", padding: "5px 0px 5px 20px", textDecoration: "none" },
         desc: { padding: "0 15px 0 35px", fontSize: "9pt", marginBottom: ".5em" },
         header: { fontSize: "medium", color: "rgba(146,208,80,.8)" },
         list: { paddingLeft: "0", listStyleType: "none", margin: "0 -15px" },
         listItem: { paddingLeft: "20px" },
         bullet: { color: "rgba(255,205,0,.8)", transform: "scale(.5)" },
         copyright: { color: "rgb(125,135,140)", fontSize: "8pt" }
      }

      const setSelectedLink = (linkId) =>  this.setState({selectedLink: linkId});

      const showLinks = links => links.map(link =>
         <li key={link.Id}>
            <RouteLink vm={this.vm} route={link.Route}
                       style={link.Id == this.state.selectedLink ? styles.activeExampleLink : styles.exampleLink} 
                       className="example-link"
                       onClick={() => setSelectedLink(link.Id)}>
               <span style={styles.bullet} className="glyphicon glyphicon-asterisk"></span>
               {link.Caption}
            </RouteLink>
            <div style={styles.desc}>{link.Description}</div>
         </li>
      );

      return (
         <div style={styles.navMenu}>
            <div>
               <h3 style={styles.header}>{this.state.LocalizedStrings.ProjectNav}</h3>
               <ul id="BasicExamples" style={styles.list}>
                  {showLinks(this.state.BasicExampleLinks)}
               </ul>
               <h3 style={styles.header}>{this.state.LocalizedStrings.ContactNav}</h3>
               <ul id="FurtherExamples" style={styles.list}>
                  {showLinks(this.state.FurtherExampleLinks)}
               </ul>
            </div>

            <div style={styles.copyright}>
               <br />
               <small>
                  © 2017 Long Nguyen.&nbsp;
                  All code licensed under the <a style={styles.link} href="http://www.apache.org/licenses/LICENSE-2.0">Apache license version 2.0</a>
               </small>
               <br /><br />
            </div>
         </div>
      );
   }
});

var PageHeader = React.createClass({

    getInitialState() {
        // Connect this component to the back-end view model.
        this.vm = dotnetify.react.connect("PageHeaderVM", this);

        // Functions to dispatch state to the back-end.
        this.dispatch = state => this.vm.$dispatch(state);
        this.dispatchState = state => {
            this.setState(state);
            this.vm.$dispatch(state);
        }

        // This component's JSX was loaded along with the VM's initial state for faster rendering.
        var state = window.vmStates.PageHeaderVM || {};
        return state;
    },
    componentWillUnmount() {
        this.vm.$destroy();
    },
    render() {
        return (
            <div class="well">
                <h1><span class="circle"></span>&nbsp;Tearc + <span><img src="/images/reactLogo.svg" width="28" />Architect</span></h1>
                <p>{this.state.LocalizedStrings != null ? this.state.LocalizedStrings.Slogan : ""}</p>
                <MuiThemeProvider>
                    <div>
                        <div className="row">
                            <div className="col-md-12">
                                <AppBar style={{ marginBottom: "1em" }}
                                    iconElementRight={<LanguageToggle onToggle={code => this.dispatch({ CultureCode: code })} />} />
                            </div>
                        </div>
                    </div>
                </MuiThemeProvider>
            </div>
        );
    }
});

var LanguageToggle = React.createClass({
    getInitialState() {
        return {
            code: "en-US",
            language: "English"
        }
    },
    render() {
        const handleToggle = (event, checked) => {
            var code = !checked ? "en-US" : "fr-FR";
            this.setState({ code: code });
            this.setState({ language: !checked ? "English" : "Français" });
            this.props.onToggle(code);
        }

        return (
            <Toggle style={{ marginTop: "1em", width: "7em" }}
                trackSwitchedStyle={{ backgroundColor: "#e0e0e0" }}
                thumbSwitchedStyle={{ backgroundColor: "#11cde5" }}
                onToggle={handleToggle}
                label={this.state.language}
                labelStyle={{ color: "white", fontSize: "medium" }}
            />
        );
    }
});

ReactDOM.render(
  <Index />,
  document.querySelector("#NavMenu")
);

ReactDOM.render(
    <PageHeader />,
    document.querySelector("#pageHeader")
);
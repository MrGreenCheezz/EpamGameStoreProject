import React, { Component } from 'react'
import imageReplace from '../assets/test.png'
import './/ComponentsCSS/GameItemCard.css'
import eventBus from '../eventBus'

export default class GameItemCard extends Component {
  constructor(props) {
    super(props)
    this.HandleError = this.HandleError.bind(this);
    this.cardHovered = this.cardHovered.bind(this);
    this.cardEndHover = this.cardEndHover.bind(this);
    this.editIconClicked = this.editIconClicked.bind(this);
    this.closeEditMenuClicked = this.closeEditMenuClicked.bind(this);
    this.EditGameRequest = this.EditGameRequest.bind(this);
    this.DeleteGameRequest = this.DeleteGameRequest.bind(this);
    this.CallEventAddItem = this.CallEventAddItem.bind(this);
    this.state = {
      imageUrl: this.props.ImageUrl,
      MenuState: "none",
      GameName: this.props.Title,
      GameDescription: this.props.Description,
      GamePrice: this.props.Price,
      EditState: "none"
    }

  }

  cardHovered() {
    this.setState({ MenuState: "flex" })
  }

  cardEndHover() {
    this.setState({ MenuState: "none" })
  }

  editIconClicked(){
    this.setState({EditState: "flex"})
  }

  closeEditMenuClicked(){
    this.setState({EditState: "none"})
  }

  EditGameRequest() {
    const filedata = new FormData();
    filedata.append('file', '');
    fetch('http://localhost:21409/api/games/editGame?id=' + this.props.Id +
        '&name=' + this.state.GameName +
        '&description=' + this.state.GameDescription +
        '&price=' + this.state.GamePrice
        , {
            method: 'POST',
            headers: {
                'Accept' : 'application/json'
            },
            body: filedata
        }).then(response => response.json()).then(data => console.log(data))
        this.setState({EditState: "none"})
}

DeleteGameRequest() {
  const filedata = new FormData();
  filedata.append('file', '');
  fetch('http://localhost:21409/api/games/deleteGame?id=' + this.props.Id
      , {
          method: 'POST',
          headers: {
              'Accept' : 'application/json'
          },
          body: filedata
      }).then(response => response.json()).then(data => console.log(data))
      window.location.reload(false);
}

  componentDidMount() {
    if (this.state.imageUrl == null) {
      this.HandleError();
    }
  }

  HandleError() {
    this.setState({ imageUrl: 'https://raw.githubusercontent.com/openintents/filemanager/master/promotion/icons/ic_launcher_filemanager_512.png' });
  }

  CallEventAddItem(){
    eventBus.dispatch("ItemAdded", { Id:this.props.Id, Name: this.props.Title, Price: this.props.Price, ImageUrl: this.props.ImageUrl });
  }

  render() {
    return (
      <div className="card text-bg-dark Custom" style={{ width: "280px", height: "fit-content", marginTop: 30 }} onMouseEnter={this.cardHovered} onMouseLeave={this.cardEndHover}>
        <object data={this.state.imageUrl} type='image/png' className="card-img" style={{ width: "100%", height: 300, position: "relative", pointerEvents: "none" }}
          onError={this.HandleError}  >
          <img src='https://raw.githubusercontent.com/openintents/filemanager/master/promotion/icons/ic_launcher_filemanager_512.png'
            className="card-img" alt="..." style={{ height: 300 }}></img>
        </object>
        <div className="card-img-overlay custom-bg CardMenu">
          <div style={{width:"100%"}}>
            <a className="card-text text-button" style={{ fontSize: 20 }} href={"/game/" + this.props.Id} target="_blank">{this.state.GameName}</a>
            <h5 className="card-title CardPrice">$ {this.state.GamePrice}</h5>
          </div>
          <div style={{justifyContent:"flex-end"}}>
            <button type="button" className="btn btn-success" onClick={this.CallEventAddItem}>Buy!</button>
          </div>
        </div>
        <div className='HoveringMenu' style={{ display: this.state.MenuState }}>
          <img src='https://www.pdfzorro.com/Images/IconsFunktionen/pdf-edit.webp' onClick={this.editIconClicked}></img>
          <img src='https://cdn-icons-png.flaticon.com/512/542/542724.png' onClick={this.DeleteGameRequest}></img>
        </div>
        <div className='EditGameScreen' style={{display: this.state.EditState}} >
          <div className='EditGameForm'>
              <div>
                <b style={{fontSize: 35, color:"black"}}>Edit Game</b>
                <div className='row AddGameField'>
                  <input value={this.state.GameName} placeholder={"Game name.."}
                    onChange={(event) => this.setState({ GameName: event.target.value })}></input>
                </div>
                <div className='row AddGameField'>
                  <input value={this.state.GameDescription} placeholder={"Game description.."}
                    onChange={(event) => this.setState({ GameDescription: event.target.value })}></input>
                </div>
                <div className='row AddGameField'>
                  <input value={this.state.GamePrice} placeholder={"Game price.."}
                    onChange={(event) => this.setState({ GamePrice: event.target.value })} type="number"></input>
                </div>
                <div className='row'>
                  <button type="button" className='btn btn-success' style={{ width: 110, height: 40, marginTop: 10 }} onClick={this.EditGameRequest}>EditGame</button>
                  <button type="button" className='btn btn-success' style={{ width: 110, height: 40, marginTop: 10 }} onClick={this.closeEditMenuClicked}>CloseMenu</button>
                </div>
              </div>
          </div>
        </div>
      </div>
    )
  }
}

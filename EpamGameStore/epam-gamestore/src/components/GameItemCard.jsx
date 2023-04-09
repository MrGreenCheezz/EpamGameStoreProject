import React, { Component } from 'react'
import imageReplace from '../assets/test.png'
import './/ComponentsCSS/GameItemCard.css'

export default class GameItemCard extends Component {
  constructor(props) {
    super(props)
    this.HandleError = this.HandleError.bind(this);
    this.state = {
      imageUrl: this.props.ImageUrl
    }

  }

  componentDidMount(){
    if(this.state.imageUrl == null){
      this.HandleError();
    }
  }

  HandleError(){
    this.setState({imageUrl: 'https://raw.githubusercontent.com/openintents/filemanager/master/promotion/icons/ic_launcher_filemanager_512.png'});
  }

  render() {
    return (
      <div className="card text-bg-dark Custom" style={{ width: "fit-content", height: "fit-content", marginTop: 30 }}>
        <object data={this.state.imageUrl} type='image/png' className="card-img" style={{ width: "fit-content", height: 300 }} onError={this.HandleError}>
          <img src='https://raw.githubusercontent.com/openintents/filemanager/master/promotion/icons/ic_launcher_filemanager_512.png' className="card-img" alt="..." style={{ height: 300 }}></img>
        </object>
        <div className="card-img-overlay custom-bg">
          <a className="card-text" style={{ fontSize: 20 }} href={"/game/" + this.props.Id}>{this.props.Title}</a>
          <h5 className="card-title CardPrice">$ {this.props.Price}</h5>
        </div>
      </div>
    )
  }
}

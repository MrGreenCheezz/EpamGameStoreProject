import React, { Component } from 'react'
import './ComponentsCSS/FullGameViewComponent.css'

export default class FullGameViewComponent extends Component {
    constructor(props) {
        super(props)

        this.state = {
            GameName: "",
            GamePrice: 0,
            GameDescription: "",
            GameId: 0,
            GameGenres: "",
            ImageUrl: ""
        }
    }

    async componentDidMount() {
        const result = await this.GetItemFromApi(this.props.Id);
        this.setState({
            GameName: result.name,
            GamePrice: result.price,
            GameDescription: result.description, GameId: result.id, GameGenres: result.genres, ImageUrl: result.imageUrl
        });
        if(this.state.ImageUrl == null){
            this.setState({ImageUrl: "https://raw.githubusercontent.com/openintents/filemanager/master/promotion/icons/ic_launcher_filemanager_512.png"});
        }
    }

    async GetItemFromApi(id) {
        const response = await fetch("http://localhost:21409/api/games/getGame?id=" + id);
        const jsonResult = await response.json()
        return jsonResult;
    }

    render() {
        return (
            <div className='GameMainContainer'>
                <div className='GameCard'>
                    <div className='GamePicture'>
                        <img src={this.state.ImageUrl}></img>
                    </div>
                    <div className='GameInfo'>
                        <div className='GameCardHeader'>
                            <div>
                                <div className='GameName'>
                                    <p>{this.state.GameName}</p>
                                </div>
                                <div className='GamePrice'>
                                    <p>$ {this.state.GamePrice}</p>
                                </div>
                                <div className='GameGenres'>
                                    <p>Game genres: {this.state.GameGenres}</p>
                                </div>
                            </div>
                            <div className='BuyButtonSection'>
                                <button type="button" className='btn btn-success' style={{ width: 150, height: 50 }}>Buy!</button>
                            </div>
                        </div>
                        <hr style={{ "height": "2px", "width": "100%", "borderWidth": 0, "color": "white", "backgroundColor": "white" }}></hr>
                        <div className='GameDescription'>
                            <p>{this.state.GameDescription}</p>
                        </div>
                    </div>
                </div>
                <hr style={{ "height": "2px", "width": "100%", "borderWidth": 0, "color": "white", "backgroundColor": "white" }}></hr>
                <p style={{ color: "white" }}>Here belongs comment section, they will be later!</p>
            </div>
        )
    }
}

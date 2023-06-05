import React, { Component } from 'react'
import CommentSectionComponent from './CommentSectionComponent';
import './ComponentsCSS/FullGameViewComponent.css'

export default class FullGameViewComponent extends Component {
    constructor(props) {
        super(props)
        this.fileInputRef = React.createRef();
        this.handleImageClick = this.handleImageClick.bind(this);
        this.onFileInputChange = this.onFileInputChange.bind(this);
        this.GetGenresFromApi = this.GetGenresFromApi.bind(this);
        this.state = {
            GameName: "",
            GamePrice: 0,
            GameDescription: "",
            GameId: 0,
            GameGenres: "",
            ImageUrl: "",
            ImageFile: null
        }
    }

    handleImageClick(){
        this.fileInputRef.current.click();
    }

    onFileInputChange(event){
        this.setState({ImageFile: event.target.files[0]});
        this.EditGameRequest(event.target.files[0]);
        window.location.reload(false);
    }

    
    EditGameRequest(file) {
        const filedata = new FormData();
        filedata.append('file', file);
        fetch('http://localhost:21409/api/games/editGame?id=' + this.state.GameId +
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
    }


    async componentDidMount() {
        const result = await this.GetItemFromApi(this.props.Id);
        this.setState({
            GameName: result.name,
            GamePrice: result.price,
            GameDescription: result.description, GameId: result.id, GameGenres: result.genres, ImageUrl: result.imageUrl
        });
        const genres = await this.GetGenresFromApi(this.props.Id);
        var genresString = "";
        genres.map(genre => {
            genresString += genre.name + " ";     
        })
        this.setState({GameGenres: genresString});
        if(this.state.ImageUrl == null){
            this.setState({ImageUrl: "https://raw.githubusercontent.com/openintents/filemanager/master/promotion/icons/ic_launcher_filemanager_512.png"});
        }
    }

    async GetItemFromApi(id) {
        const response = await fetch("http://localhost:21409/api/games/getGame?id=" + id);
        const jsonResult = await response.json()
        return jsonResult;
    }
    async GetGenresFromApi(id) {
        const response = await fetch("http://localhost:21409/api/genres/getGameGenres?gameId=" + id);
        const jsonResult = await response.json()
        return jsonResult;
    }

    render() {
        return (
            <div className='GameMainContainer'>
                <div className='GameCard'>
                    <div className='GamePicture'>
                        <img src={this.state.ImageUrl}></img>
                        <div className='AddImageButton'>
                            <input type="image" src="https://pngimg.com/d/plus_PNG106.png" style={{width: 80,height:"85%"}}
                             ref={(ref) => { this.imageInput = ref }} onClick={this.handleImageClick}/>   
                            <input type="file" id="my_file" style={{display: "none",width: 80, height:"40%"}} ref={this.fileInputRef} onChange={this.onFileInputChange} />                                                  
                        </div>
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
                            <p>Game description:</p>
                            <p>{this.state.GameDescription}</p>
                        </div>
                    </div>
                </div>
                <hr style={{ "height": "2px", "width": "100%", "borderWidth": 0, "color": "white", "backgroundColor": "white" }}></hr>
                <CommentSectionComponent Id={this.props.Id}></CommentSectionComponent>
            </div>
        )
    }
}

import React, { Component } from 'react'
import './ComponentsCSS/AddGame.css'

export default class AddGameComponent extends Component {
    constructor(props) {
        super(props)
        this.ChangeShowState = this.ChangeShowState.bind(this);
        this.AddGameRequest = this.AddGameRequest.bind(this);

        this.state = {
            IsShowed: false,
            GameName: "",
            GameDescription: "",
            GamePrice: 0,
        }
    }

    AddGameRequest() {
        fetch('http://localhost:21409/api/games/addGame?name=' + this.state.GameName +
            '&description=' + this.state.GameDescription +
            '&price=' + this.state.GamePrice +
            '&genres=' + "", {
                method: 'POST',
                headers: {
                    'Accept' : 'application/json'
                }
            }).then(response => response.json()).then(data => console.log(data))
            this.setState({IsShowed: false, GameName: "", GameDescription: "", GamePrice: 0})
    }

    ChangeShowState() {
        this.setState({ IsShowed: !this.state.IsShowed });
        console.log("kek")
    }

    render() {
        switch (this.state.IsShowed) {
            case true:
                return (
                    <div className='AddMenu'>
                        <button type="button" className='btn btn-success' style={{ width: 150, height: 50, marginLeft: "auto", marginRight: 0, marginTop: 10 }} onClick={this.ChangeShowState} >Add game</button>
                        <div className='AddMenu'>
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
                            <div className='row AddGameField'>
                                <button type="button" className='btn btn-success' style={{ width: 50, height: 40, marginTop: 10 }} onClick={this.AddGameRequest}>Add</button>
                            </div>
                        </div>
                    </div>
                )
            case false:
                return (
                    <div className='AddMenu'>
                        <button type="button" className='btn btn-success' style={{ width: 150, height: 50, marginLeft: "auto", marginRight: 0, marginTop: 10 }} onClick={this.ChangeShowState} >Add game</button>
                    </div>
                )
        }

    }
}

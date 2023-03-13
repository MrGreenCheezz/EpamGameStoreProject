import React, { Component } from 'react'
import GameItemCard from './GameItemCard';
import './GameShowcase.css'

export default class GamesShowcase extends Component {
    constructor(props) {
      super(props)
    
      this.state = {
         Items:[],
         MaxItemsOnPage: 10,
         CurrentPagination: 0
      }
    }

   async componentDidMount(){
        const responseItems = await this.GetItemsFromApi();
        this.setState({Items: responseItems});
    }

    async GetItemsFromApi(){
        const response = await fetch("http://localhost:21409/getItems?amount="+this.state.MaxItemsOnPage+"&offset="+this.state.CurrentPagination);
        const jsonResult = await response.json()
        return jsonResult;
    }

  render() {
    return (
      <div className='MainShowcase'>
        {this.state.Items.map(item => (
            <GameItemCard Title={item.name} Price={item.price} key={item.id}></GameItemCard>
        ))}
      </div>
    )
  }
}

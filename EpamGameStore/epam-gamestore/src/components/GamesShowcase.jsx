import React, { Component } from 'react'
import GameItemCard from './GameItemCard';
import './ComponentsCSS/GameShowcase.css'
import PaginationComponent from './PaginationComponent';

export default class GamesShowcase extends Component {
  constructor(props) {
    super(props)
    this.ChangeCurrentPage = this.ChangeCurrentPage.bind(this);

    this.state = {
      Items: [],
      MaxItemsOnPage: 10,
      CurrentPagination: 0
    }
  }

async ChangeCurrentPage(newPage){
    var NewPageCount = this.state.CurrentPagination + newPage;
    if(NewPageCount <= 0){
      this.setState({CurrentPagination: 0});
      NewPageCount = 0;
    }
    else{
      this.setState({CurrentPagination: NewPageCount});
    }
    const responseItems = await this.GetItemsFromApi(NewPageCount);
    this.setState({ Items: responseItems });
  }

  async componentDidMount() {
    const responseItems = await this.GetItemsFromApi(0);
    this.setState({ Items: responseItems });
  }

  async GetItemsFromApi(offset) {
    const response = await fetch("http://localhost:21409/getItems?amount=" + this.state.MaxItemsOnPage + "&offset=" + offset);
    const jsonResult = await response.json()
    return jsonResult;
  }

  render() {
    return (
      <div className='MainShowcase'>
        <div className='CardContainer'>
          {this.state.Items.map(item => (
            <GameItemCard Title={item.name} Price={item.price} key={item.id} Id={item.id}></GameItemCard>
          ))}
        </div>
        <div className='Pagination'>
          <PaginationComponent ChangeFunc={this.ChangeCurrentPage}></PaginationComponent>
        </div>
      </div>


    )
  }
}

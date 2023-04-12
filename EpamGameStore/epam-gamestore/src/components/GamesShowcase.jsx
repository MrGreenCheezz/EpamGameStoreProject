import React, { Component } from 'react'
import GameItemCard from './GameItemCard';
import './ComponentsCSS/GameShowcase.css'
import PaginationComponent from './PaginationComponent';
import AddGameComponent from './AddGameComponent';

export default class GamesShowcase extends Component {
  constructor(props) {
    super(props)
    this.ChangeCurrentPage = this.ChangeCurrentPage.bind(this);
    this.AddGameButtonClicked = this.AddGameButtonClicked.bind(this);

    this.state = {
      Items: [],
      MaxItemsOnPage: 10,
      CurrentPagination: 0,
      ShowAddGame: false,
    }
  }

  async ChangeCurrentPage(newPage) {
    var NewPageCount = this.state.CurrentPagination + newPage;
    if (NewPageCount <= 0) {
      this.setState({ CurrentPagination: 0 });
      NewPageCount = 0;
    }
    else {
      this.setState({ CurrentPagination: NewPageCount });
    }
    const responseItems = await this.GetItemsFromApi(NewPageCount);
    this.setState({ Items: responseItems });
  }

  async componentDidMount() {
    const responseItems = await this.GetItemsFromApi(0);
    this.setState({ Items: responseItems });
  }

  async GetItemsFromApi(offset) {
    const response = await fetch("http://localhost:21409/api/games/getGames?amount=" + this.state.MaxItemsOnPage + "&offset=" + offset);
    const jsonResult = await response.json()
    return jsonResult;
  }

  AddGameButtonClicked() {
    this.setState({ ShowAddGame: !this.state.ShowAddGame })
  }

  render() {
    return (
      <div className='MainShowcase'>
        <div className='ShowcaseHeader'>
          <div className="input-group flex-nowrap" style={{ width: 170, height: 25, marginRight: 0, marginLeft: "auto" }}>
            <span className="input-group-text" id="addon-wrapping" style={{ borderColor: "grey", backgroundColor: "gray" }}>&#128269;</span>
            <input type="text" className="form-control test" placeholder="" aria-label="Search" aria-describedby="addon-wrapping" style={{ borderColor: "grey", backgroundColor: "gray", color: "white" }}></input>
          </div>
          <AddGameComponent></AddGameComponent>

        </div>
        <div className='CardContainer'>
          {this.state.Items.map(item => (
            <GameItemCard Title={item.name} Price={item.price} key={item.id} Id={item.id} ImageUrl={item.imageUrl} Description={item.description}></GameItemCard>
          ))}
        </div>
        <div className='Pagination'>
          <PaginationComponent ChangeFunc={this.ChangeCurrentPage}></PaginationComponent>
        </div>
      </div>


    )
  }
}

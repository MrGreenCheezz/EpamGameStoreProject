import React, { Component } from 'react'

export default class PaginationComponent extends Component {
    constructor(props) {
        super(props)
        this.ChangeCurrentPage = this.ChangeCurrentPage.bind(this);
        this.state = {
            CurrentPage: 0
        }
    }

    ChangeCurrentPage(amount){
       const newPageCount = this.state.CurrentPage + amount;
        if(newPageCount <= 0) {
            this.setState({CurrentPage: 0});
        }
        else{
            this.setState({CurrentPage: newPageCount});
        }
        this.props.ChangeFunc(amount);
    }

    render() {
        return (
            <div>
                <nav aria-label="Page navigation">
                    <ul className="pagination" style={{"--bs-pagination-bg": "black", "--bs-pagination-color": "white"}}>
                        <li className="page-item">
                            <a className="page-link"  aria-label="Previous" onClick={()=> this.ChangeCurrentPage(-1)}>
                                <span aria-hidden="true">&laquo;</span>
                            </a>
                        </li>
                        <li className="page-item"><p className='page-link'>{this.state.CurrentPage}</p></li>
                        <li className="page-item">
                            <a className="page-link"  aria-label="Next" onClick={()=> this.ChangeCurrentPage(1)}>
                                <span aria-hidden="true">&raquo;</span>
                            </a>
                        </li>
                    </ul>
                </nav>
            </div>
        )
    }
}

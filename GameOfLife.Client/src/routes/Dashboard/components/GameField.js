import React from 'react'
import {
  Grid,
  Row,
  Col,
  FormGroup,
  FormControl,
  ControlLabel,
  ButtonToolbar,
  ButtonGroup,
  Checkbox,
  Button
} from 'react-bootstrap'

export const GameField = (props) => {
  var namesList = props.field.map(function(row, index) {
    return React.DOM.tr({
      key: index
    }, row.map(function(col, index2) {
      return React.DOM.td({key: index2},<div onClick={props.loading || props.animation? null : props.onFieldChange} disabled={props.loading || props.animation} x={index} y={index2} className={`point ${col == 0? '' : 'active'}`} ></div>);
    }));
  });

  return (
    <table className='game-field'>
      <tbody>
        {namesList}
      </tbody>
    </table>
  )
}

export default GameField

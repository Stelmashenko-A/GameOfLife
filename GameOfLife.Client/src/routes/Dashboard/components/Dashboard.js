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

import './Dashboard.scss'
import GameField from './GameField'

export const Dashboard = (props) => {

  return (
    <div>
      <Col xs={12}>
        <div className='dashboard'>
          Dashboard
        </div>
      </Col>
      <Col xs={12}>
        <div className='game-board'>
          <GameField field={props.field} onFieldChange={props.onFieldChange} loading={props.loading} />
        </div>
      </Col>
      <Col xs={12}>
        <div className='settings'>
          <Row>
            <Col xs={4}>
              <FormGroup>
                <FormControl type="number" min={0} defaultValue={props.steps} disabled={props.loading || props.loaded} placeholder="Steps" onChange={props.onStepsChange}/>
              </FormGroup>
            </Col>
            <Col xs={4}>
              <FormGroup>
                <FormControl type="number" min={1} defaultValue={props.parts} disabled={props.loading || props.loaded} placeholder="Parts" onChange={props.onPartsChange}/>
              </FormGroup>
            </Col>
            <Col xs={4}>
				<FormGroup>
				  <FormControl type="number" min={1} defaultValue={props.fieldSize} disabled={props.loading || props.loaded} placeholder="Size" onChange={props.onSizeChange}/>
				</FormGroup>
            </Col>
          </Row>
        </div>
      </Col>
      <Col xs={12}>
        <div className='actions'>
          <ButtonToolbar>
            <ButtonGroup>
              <Button disabled={props.loading || !props.loaded} onClick={props.prevButtonMaxHandler}>{'<<'}</Button>
              <Button disabled={props.loading || !props.loaded}  onClick={props.prevButtonHandler}>{'<'}</Button>
            </ButtonGroup>
            <ButtonGroup>
              <Button disabled>{props.currentStep}</Button>
            </ButtonGroup>
            <ButtonGroup>
              <Button disabled={props.loading || !props.loaded}  onClick={props.nextButtonHandler}>{'>'}</Button>
              <Button disabled={props.loading || !props.loaded}  onClick={props.nextButtonMaxHandler}>{'>>'}</Button>
            </ButtonGroup>

			<ButtonGroup className='pull-right'>
              <Button disabled={props.loading} onClick={props.calculateHandler}>Calculate</Button>
            </ButtonGroup>
          </ButtonToolbar>
        </div>
      </Col>
    </div>
  )
}

export default Dashboard

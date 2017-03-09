import React from 'react'
import {
  Row,
  Col,
  FormGroup,
  FormControl,
  ButtonToolbar,
  ButtonGroup,
  Button,
  Alert
} from 'react-bootstrap'

import './Dashboard.scss'
import GameField from './GameField'

export const Dashboard = (props) => {
  return (
    <Row>
      <Col xs={12}>
        <div className='dashboard'>
          Dashboard
        </div>
      </Col>
      <Col xs={12}>
        <div className='game-board'>
          <GameField field={props.field} onFieldChange={props.onFieldChange} loading={props.loading} animation={props.animation}/>
        </div>
      </Col>
      <Col xs={12}>
        <div className='settings'>
          <Row>
            <Col xs={4}>
              <FormGroup>
                <FormControl type='number' min={0} defaultValue={props.steps} disabled={props.loading} placeholder='Steps' onChange={props.onStepsChange}/>
              </FormGroup>
            </Col>
            <Col xs={4}>
              <FormGroup>
                <FormControl type='number' min={1} defaultValue={props.parts} disabled={props.loading} placeholder='Parts' onChange={props.onPartsChange}/>
              </FormGroup>
            </Col>
            <Col xs={4}>
              <FormGroup>
                <FormControl type='number' min={1} defaultValue={props.fieldSize} disabled={props.loading} placeholder='Size' onChange={props.onSizeChange}/>
              </FormGroup>
            </Col>
          </Row>
        </div>
      </Col>
      <Col xs={12} className={props.error
        ? ''
        : 'hidden'}>
        <Alert bsStyle='danger'>
          <h4>Oh snap! You got an error!</h4>
          <p>{props.error}</p>
          <p>
            <Button bsStyle='default' onClick={props.hideErrorHandler}>{'Ok'}</Button>
          </p>
        </Alert>
      </Col>
      <Col xs={12} className={props.host
        ? ''
        : 'hidden'}>
        <div>
          <h4>Host: {props.host}
            <span className={props.loaded
              ? 'text-success'
              : 'text-info'}>{props.partsLoaded === props.parts
                ? ' processed '
                : ' processes '}
              your request</span>
          </h4>
          <h5 className={props.partsLoaded === props.parts
            ? 'hidden'
            : ''}>Part {props.partsLoaded} {' '}
             of {props.parts}  {' '}
            loaded</h5>
          <h5 className={props.partsLoaded === props.parts
            ? ''
            : 'hidden'}>All {' '} {props.parts} {' '}
            parts was successfuly loaded you can view result above.</h5>
        </div>
      </Col>
      <Col xs={12}>
        <div className='actions'>
          <ButtonToolbar>
            <ButtonGroup>
              <Button disabled={!props.loaded || props.animation} onClick={props.prevButtonMaxHandler}>{'<<'}</Button>
              <Button disabled={!props.loaded || props.animation} onClick={props.prevButtonHandler}>{'<'}</Button>
            </ButtonGroup>
            <ButtonGroup>
              <Button disabled>{props.currentStep}</Button>
            </ButtonGroup>
            <ButtonGroup>
              <Button disabled={!props.loaded || props.animation} onClick={props.nextButtonHandler}>{'>'}</Button>
              <Button disabled={!props.loaded || props.animation} onClick={props.nextButtonMaxHandler}>{'>>'}</Button>
            </ButtonGroup>
            <ButtonGroup className='pull-right'>
              <Button disabled={props.loading || !props.loaded || props.animation} onClick={props.resetButtonHandler} bsStyle='danger'>Reset</Button>
              <Button disabled={props.loading || props.loaded || props.animation} onClick={props.calculateHandler} bsStyle='success'>Calculate</Button>
            </ButtonGroup>
          </ButtonToolbar>
        </div>
      </Col>
    </Row>
  )
}

export default Dashboard

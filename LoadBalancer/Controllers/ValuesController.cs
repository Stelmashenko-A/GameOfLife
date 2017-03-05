using System;
using System.Web.Http;
using LoadBalancer.Models;

namespace LoadBalancer.Controllers
{
    public class ValuesController : ApiController
    {
        
        // GET api/values
        public Guid Process(RequestForProcessing request)
        {
            /* сгенерить айди для запроса
             * сходить в RouteTable
             * найти свободный хост (проверить последнее время отклика, если что пропинговать и выкинуть недоступные)
             * записать айди запроса
             * пометить как занятый
             * сохранить
             * отправить данные на урл хоста*/
             

            return request.Id;
        }

        public string Get(Guid task, int part)
        {
            /*найти хост, который обрабатывает данные
             * запросить данные
             * переслать клиенту*/
            return "";
        }

        public Hosts GetHosts()
        {
            /*из таблицы дастать инфу о хостах и тасках которые они выполняют
             * отправить на клиерт
             */
            return null;
        }
       
    }
}

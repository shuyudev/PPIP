/**
 * TaskController
 *
 * @description :: Server-side logic for managing tasks
 * @help        :: See http://links.sailsjs.org/docs/controllers
 */

module.exports = {

  fetch: function (req, res) {
    var phoneId = req.param('phoneId');

    if (!phoneId) {
      return res.missingFields(['phoneId']);
    }

    Task.update({
        phone: phoneId,
        status: 'notStarted'
      }, {
        status: 'inProgress'
      })
    .exec(function (err, tasks) {
      if (err) {
        return res.serverError(err);
      }

      res.status(200).json(tasks);
    });
  }

};


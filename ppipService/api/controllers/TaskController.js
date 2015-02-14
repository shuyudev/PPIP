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

    sails.log.debug(phoneId);

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
  },

  uploadComplete: function (req, res) {
    var taskId = req.param('id');

    var phoneId = req.param('phoneId');
    var blobName = req.param('blobName');

    if (!taskId || !phoneId  || !blobName) {
      return res.missingFields(['id', 'phoneId', 'blobName']);
    }

    Task.findOne({
      id: taskId,
      phone: phoneId,
      type: 'upload',
      status: 'inProgress'
    })
    .exec(function (err, uploadTask) {
      if (err) {
        return res.serverError(err);
      }

      if (!uploadTask) {
        return res.notFound();
      }

      Pipeline.findOne({id: uploadTask.pipeline})
      .populate('input')
      .populate('output')
      .exec(function (err, pipeline) {

        Task.create(Task.newDownloadTasks(pipeline, blobName))
        .exec(function (err, tasks) {
          if (err) {
            return res.serverError(err);
          }

          return res.status(200).json(tasks);
        });

      });
    });
  }

};


# Contributing

We welcome pull requests from anyone with an idea. In fact, if you're short of an idea, why not take a look at our [issues]?

### TL;DR...

1. Use [issues] to stake your claim
2. Fork the repo
3. Work on `develop`
4. Test all the things
5. Raise a pull request 

## Before you fork...

The best way to start contributing is to start a conversation:

- If there's an open issue that you'd like to fix, it would be a good idea to state your intention.
- If you've got a brand new idea that's not on our list, it's best to start by adding it to the list.

Either way, we can then assign the issue to you. This prevents you from wasting your time working on something at the same time as someone else. This also provides an opportunity (if you'd like) to talk about your intended approach and to ask any questions you might have.

## All systems go!
Once you've decided on an issue and have it assigned to you, it's time to fork the project into your own repo.

Get it downloaded via whatever means you prefer and then take a moment to make sure it's all running properly; running the tests and the demos is usually a good start.

Next, it's time to get coding. Implement your changes, make sure they're well tested (see below), and commit them.

Once you're happy that you've implemented and tested your change, push your commits back up to your repo. Now you can raise a pull request.

## Good pull requests
A good pull request (PR) is one that is easy to review. You want the benefits of your change to be obvious. The person reviewing your PR wants to be confident that they fully understand your change and to know that you haven't introduced any regressions.

Some tips for a good PR:

- Reference the issue, so we can see the problem you're trying to solve
- One issue per PR, so we don't let a change that needs work hold a good change hostage
- Keep your change focused. (*Tools like ReSharper may offer to make sweeping changes, but this can introduce noise to a PR, so please avoid this unless explicitly addressing a "clean-up" issue.*)
- Proactively explain anything that's likely to cause confusion or concern.

## Testing
Please run the unit tests before you raise a pull request. They should all pass.

We aim for 100% coverage of ConTabs code by tests, so please add unit tests to cover any new code you add.

To power our tests, we use NUnit 3 and Shouldly. For test coverage analysis, we use OpenCover and send the data to [Coveralls].

## Branches

Development happens on the `develop` branch. Please make your commits on develop and target develop when you make a pull request.

Master will reflect the publicly released codebase and will be tagged with versions corresponding to those available in [NuGet].

## Contributor behaviour

By participating in this project, you agree to abide by the ConTabs [code of conduct].

More importantly though, remember that we're all doing this in our spare time. Be nice to each other and try to have fun!

[code of conduct]: CODE_OF_CONDUCT.md
[issues]: https://github.com/tdwright/contabs/issues
[NuGet]: https://www.nuget.org/packages/ConTabs.tdwright/
[Coveralls]: https://coveralls.io/github/tdwright/contabs
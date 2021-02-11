  <!-- <a href="https://xadrezconsole.netlify.app">Demo</a> -->
</div>

<h1 align="center">Xadrez Console</h1>

<p align="horizontal">

<a href="https://github.com/ewertho/xadrez-console/issues"><img alt="GitHub issues" src="https://img.shields.io/github/issues/ewertho/xadrez-console"></a>

<a href="https://github.com/ewertho/xadrez-console/stargazers"><img alt="GitHub stars" src="https://img.shields.io/github/stars/ewertho/xadrez-console?style=social&maxAge=2592000"></a>

<a href="https://github.com/ewertho/xadrez-console/network"><img alt="GitHub forks" src="https://img.shields.io/github/forks/ewertho/xadrez-console?style=social"></a>

<a href="https://github.com/ewertho/xadrez-console"><img alt="GitHub license" src="https://img.shields.io/github/license/ewertho/xadrez-console"></a>

</p>

<!-- Status -->

<h4 align="center">
	🚧  Xadrez Console 🚀 Under construction...  🚧
</h4>

<hr>

<p align="center">
  <a href="#dart-about">About</a> &#xa0; | &#xa0; 
  <a href="#sparkler-features">Features</a> &#xa0; | &#xa0;
  <a href="#rocket-technologies">Technologies</a> &#xa0; | &#xa0;
  <a href="#white_check_mark-requirements">Requirements</a> &#xa0; | &#xa0;
  <a href="#checkered_flag-starting">Starting</a> &#xa0; | &#xa0;
  <a href="#memo-license">License</a> &#xa0; | &#xa0;
  <a href="https://github.com/ewertho" target="_blank">Author</a>
</p>

<br>

## :dart: About

chess game made in C # and running on a terminal

<br>

## :sparkles: Features

:heavy_check_mark: Feature 1

<br>

## :rocket: Technologies

The following tools were used in this project:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)

<br>

## :white_check_mark: Requirements

Before starting :checkered_flag:, you need to have [.Net SDK](https://dotnet.microsoft.com/download) and/or [Docker](https://www.docker.com//) installed.

<br>

## :checkered_flag: Starting

```bash
# Clone this project
$ git clone https://github.com/ewertho/xadrez-console

# Access
$ cd xadrez-console

# Docker build images
$ docker build -t xadrez-image -f Dockerfile .

# Docker open consol
$ docker run -it --rm --entrypoint "bash" xadrez-image

# Run the project in consol
$ dotnet xadrez-console.dll

# Run the project whitout docker
$ dotnet run

```

## :memo: License

This project is under license from MIT. For more details, see the [LICENSE](LICENSE.md) file.

Made with :heart: by <a href="https://github.com/ewertho" target="_blank">Ewerton</a>

&#xa0;

<a href="#top">Back to top</a>
